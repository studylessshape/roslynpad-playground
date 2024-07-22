void Swap<T>(T[] arr, int start1, int count1, int start2, int count2)
{
    var t1 = arr.Take(start1).ToList();
    var t2 = arr.Skip(start1).Take(count1);
    var t3 = arr.Skip(start1 + count1).Take(start2 - (start1 + count1));
    var t4 = arr.Skip(start2).Take(count2);
    var t5 = arr.Skip(start2 + count2).Take(arr.Length - (start2 + count2));
    t1.AddRange(t4);
    t1.AddRange(t3);
    t1.AddRange(t2);
    t1.AddRange(t5);

    //Array.Copy(t1.ToArray(), arr, arr.Length);
    t1.CopyTo(arr);
}

var arr = new byte[] {1,2,3,4,5,6,7,8,9,10};
arr.Dump("before");
Swap(arr, 1, 0, 10, 1);
arr.Dump("after");

var littleEndian = new char[] {'D','C','B','A','D','C','B','A','D','C','B','A'};
littleEndian.Dump("little endian");
for(int i = 0;i < littleEndian.Length / 4; i++)
{
    Array.Reverse(littleEndian, i * 4, 4);
}
littleEndian.Dump("big endian");