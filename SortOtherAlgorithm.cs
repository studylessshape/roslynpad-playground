
var column = 8;
var list = Enumerable.Range(4, 7);
ColumnIndexSort(column, list).Dump("old");
ColumnSortNew(column, list).Dump("new");

int[] ColumnIndexSort(int column, IEnumerable<int> list)
{
    var newArr = new List<int>();
    var old = list.ToList();

    int right = old.IndexOf(column);
    int left = right - 1;

    if (right < 0)
    {
        if (column >= old.Last())
        {
            newArr.AddRange(old.OrderByDescending(k => k));
        }
        else
        {
            newArr.AddRange(old);
        }
    }
    else
    {
        for (; right < old.Count && left >= 0; right++, left--)
        {
            newArr.Add(old[right]);
            newArr.Add(old[left]);
        }

        if (right < old.Count)
        {
            newArr.AddRange(old.GetRange(right, old.Count - right));
        }
        if (left >= 0)
        {
            newArr.AddRange(old.GetRange(0, left + 1).OrderByDescending(k => k));
        }
    }

    return newArr.ToArray();
}

int[] ColumnSortNew(int column, IEnumerable<int> soureList)
{
    int e = 0;
    
    var list = soureList.ToList();
    int right = list.IndexOf(column);
    int left = right - 1;
    
    if (right < 0)
    {
        if (column >= list.Last())
        {
            return list.OrderByDescending(k => k).ToArray();
        }
        else
        {
            return list.ToArray();
        }
    }
    
    if (left < 0)
    {
        return list.ToArray();
    }
    
    while(right < list.Count && left >= 0 && left < list.Count && e + 1 < list.Count)
    {
        (list[left], list[right]) = (list[right], list[left]);
        (list[left], list[e]) = (list[e], list[left]);
        (list[right], list[e+1]) = (list[e+1], list[right]);
        
        e = e + 2;
        if (e > right)
        {
            break;
        }
        right += 1;
        left += 1;
        if (left < e)
        {
            left = e;
            right = left + 1;
        }
    }
    if (e < list.Count)
    {
        if (e > right)
        {
            list.Sort(e, list.Count - e, new AesOrder());
        }
        else
        {
            list.Sort(e, list.Count - e, new DesOrder());
        }
    }
    
    return list.ToArray();
}

class DesOrder : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (x == y) return 0;
        if (x > y) return -1;
        return 1;
    }
}

class AesOrder : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (x == y) return 0;
        if (x > y) return 1;
        return -1;
    }
}