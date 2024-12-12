var de = 214.1223m;
var split = (de % 1m).ToString("").Split('.');
split.Dump();
var scale = 0;
if (split.Length >= 2)
{
    scale = split[1].Length;
}

Math.Pow(10, scale).Dump();

foreach(var item in Enumerable.Range(0, 1))
{
    item.Dump();
}