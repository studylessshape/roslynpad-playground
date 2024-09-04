var top = 12;
var bottom = 4;
var sortedList = ColumnIndexSort(7, [4, 6, 7, 8]);
sortedList.Dump();


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