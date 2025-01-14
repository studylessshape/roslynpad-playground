var algorithmSelection = new LocationSelection();

algorithmSelection.SelectLocation(new InventoryInput()
{
    Inventories = new IInventoryItem[]
    {
        new InventoryItem() { Column = 10, Quantity = 300 },
        new InventoryItem() { Column = 14, Quantity = 600 },
        new InventoryItem() { Column = 9, Quantity = 200 },
        new InventoryItem() { Column = 13, Quantity = 600 },
    },
    Required = 500.0m,
    CanOver = true
}).Dump("RESULT");

public class InventoryInput
{
    /// <summary>
    /// 库位和对应的数量
    /// </summary>
    public IList<IInventoryItem> Inventories { get; set; }
    /// <summary>
    /// 需要满足的数量
    /// </summary>
    public decimal Required { get; set; }
    /// <summary>
    /// 开始的列
    /// </summary>
    public int? StartColumn { get; set; } = null;

    /// <summary>
    /// 是否可以超出指定数量
    /// </summary>
    public bool CanOver { get; set; } = false;

    /// <summary>
    /// <para>小数位精度</para>
    /// <para>计算时，精度不会大于指定精度</para>
    /// <para>为 0 时，精度上限为 <see cref="byte.MaxValue"/></para>
    /// <para>为 null 时，只使用整数部分</para>
    /// </summary>
    public byte? Precision { get; set; } = 4;
}

/// <summary>
/// 查找返回结果
/// </summary>
public class InventoryResult
{
    public IList<IInventoryItem> Locations { get; set; }
    public decimal TotalQuantity { get; set; }
}

public interface IInventoryItem
{
    int Id { get; set; }
    decimal Quantity { get; set; }
    int Column { get; set; }
}

public class InventoryItem : IInventoryItem
{
    public int Id { get; set; }
    public decimal Quantity { get; set; }
    public int Column { get; set; }
    public string TrayCode { get; set; }
    public string KWCode { get; set; }
}

public class LocationSelection
{
    public LocationSelection()
    {

    }

    static IInventoryItem[] Empty()
    {
        return new IInventoryItem[0];
    }

    /// <summary>
    /// 返回选择的库位 ID
    /// </summary>
    /// <param name="inventories">库位和对应的数量</param>
    /// <param name="required">需要满足的数量</param>
    /// <param name="startColumn">开始的列</param>
    /// <returns>库位 ID</returns>
    public InventoryResult SelectLocation(InventoryInput input)
    {
        if (input.Required == 0)
        {
            return new InventoryResult
            {
                Locations = Empty(),
            };
        }

        // 当所有的都能放下时，直接返回
        if (input.Inventories.Sum(i => i.Quantity) <= input.Required)
        {
            return new InventoryResult
            {
                Locations = input.Inventories,
                TotalQuantity = input.Inventories.Sum(i => i.Quantity)
            };
        }

        IList<IInventoryItem> inventoryItems = input.Inventories.Where(inv => inv.Quantity <= input.Required).ToList();

        // 指定开始列时，会按照开始列排序
        if (input.StartColumn != null)
        {
            inventoryItems = SortByStartColumn(input.StartColumn.Value, inventoryItems);
        }

        var selected = DpSearch(input.Required, inventoryItems, input.Precision);

        var result = new InventoryResult { Locations = selected, TotalQuantity = selected.Sum(inv => inv.Quantity) };

        // 添加额外的内容，确保可以超出需要的大小
        if (input.CanOver && (result.Locations.Count == 0 || result.TotalQuantity < input.Required))
        {
            result.Locations = DirectAddOver(input.Inventories, selected.ToList(), input.Required);
            result.TotalQuantity = result.Locations.Select(i => i.Quantity).Sum();
        }

        result.Locations = SortByCollection(input.Inventories, result.Locations.ToArray());

        return result;
    }

    /// <summary>
    /// 将选择的库存按照收集顺序排序
    /// </summary>
    /// <param name="from"></param>
    /// <param name="selected"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private IInventoryItem[] SortByCollection(IList<IInventoryItem> from, IInventoryItem[] selected)
    {
        return selected.OrderBy(s => from.IndexOf(s)).ToArray();
    }

    IList<IInventoryItem> SortByStartColumn(int start, IList<IInventoryItem> inventoryItems)
    {
        var sorted = new List<IInventoryItem>();
        var tops = inventoryItems.Where(i => i.Column >= start).OrderByDescending(i => i.Column).ToList();
        var bottoms = inventoryItems.Where(i => i.Column < start).OrderBy(i => i.Column).ToList();

        while (tops.Any() || bottoms.Any())
        {
            if (tops.Any())
            {
                sorted.Add(tops[0]);
                tops.RemoveAt(0);
            }
            if (bottoms.Any())
            {
                sorted.Add(bottoms[0]);
                bottoms.RemoveAt(0);
            }
        }

        return sorted;
    }

    /// <summary>
    /// 直接加算到满足要求的数量
    /// </summary>
    /// <param name="inventoryItems"></param>
    /// <param name="required"></param>
    /// <returns></returns>
    IInventoryItem[] DirectAddOver(IList<IInventoryItem> inventoryItems, IList<IInventoryItem> currentItems, decimal required)
    {
        var total = currentItems.Sum(inv => inv.Quantity);

        foreach (var item in inventoryItems.Where(inv => !currentItems.Contains(inv)))
        {
            total += item.Quantity;
            currentItems.Add(item);
            if (total >= required)
            {
                break;
            }
        }

        return currentItems.ToArray();
    }

    decimal GetScale(IList<IInventoryItem> inventories, byte? precision)
    {
        if (precision == null)
        {
            return 1;
        }

        // 获取小数位
        var digits = inventories.Select(inv =>
        {
            var quantity = inv.Quantity;
            return quantity % 1m;
        })
        .Where(digit => digit >= 0.0001m);

        var scale = 0;
        if (digits.Any())
        {
            var format = "";
            foreach (var _ in Enumerable.Range(0, precision.Value))
            {
                format += "#";
            }
            if (format.Length > 0)
            {
                format = string.Format("0.{0}", format);
            }

            scale = digits
            .Max(digit =>
            {
                var split = digit.ToString(format).Split('.');

                if (split.Length < 2)
                {
                    return 0;
                }

                if (format.Length > 0)
                {
                    return split[1].Length;
                }
                else
                {
                    // 没有格式化精度的时候，自动抹除后面多余的 0
                    return split[1].TrimEnd('0').Length;
                }
            });
        }

        return (decimal)Math.Pow(10, scale);
    }

    IInventoryItem[] DpSearch(decimal required, IList<IInventoryItem> inventories, byte? precision)
    {
        if (inventories.Count == 0)
        {
            return new IInventoryItem[0];
        }

        var scale = GetScale(inventories, precision);
        var volumn = (int)(required * scale);
        var minQuantity = (int)(inventories.Min(inv => inv.Quantity) * scale);

        decimal[][] dp = Enumerable.Range(0, inventories.Count + 1)
            .Select(_ => Enumerable.Range(0, volumn + 1)
                            .Select(i => 0m)
                            .ToArray())
            .ToArray();
        bool[][] dps = Enumerable.Range(0, inventories.Count + 1)
                            .Select(i => Enumerable.Range(0, volumn + 1).Select(i => false).ToArray())
                            .ToArray();
        // 动态规划计算最大数量
        for (int i = 1; i <= inventories.Count; i++)
        {
            var quantity = (int)(inventories[i - 1].Quantity * scale);
            var realQuantity = inventories[i - 1].Quantity;

            for (int j = volumn; j >= 0; j--)
            {
                if (j < quantity)
                {
                    dp[i][j] = dp[i - 1][j];
                    continue;
                }
                var no = dp[i - 1][j];
                var yes = dp[i - 1][j - quantity] + realQuantity;
                dp[i][j] = Math.Max(no, yes);

                if (yes > no)
                {
                    dps[i][j] = true;
                }
            }
        }

        int volumnReq = volumn;
        List<IInventoryItem> selectItem = new();
        for (int i = inventories.Count; i >= 1; --i)
        {
            var quantity = (int)(inventories[i - 1].Quantity * scale);
            var realQuantity = inventories[i - 1].Quantity;
            if (volumnReq >= quantity)
            {
                if (dps[i][volumnReq])
                {
                    selectItem.Add(inventories[i - 1]);
                    volumnReq -= quantity;
                }
            }
        }

        return selectItem.ToArray();
    }
}