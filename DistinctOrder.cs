// 测试去重是否按照前后顺序

var fakeCodes = new string[]{ "1", "2", "3" };
var random = new Random();
var list = Enumerable.Range(0, 100).Select(i => new OrderTest() { Code = fakeCodes[random.NextInt64(0,3)], Order = i }).DistinctBy(s => s.Code).Dump();

class OrderTest
{
    public string Code { get; set; } = string.Empty;
    public int Order{ get; set; }
}