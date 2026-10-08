var test = new TestObject()
{
    KeyValues = new Dictionary<string, TestChild>(),
    Name = "Test"
};

test.KeyValues.Add("v1", new TestChild() { Value1 = "v1v1", Value2 = "v1v2" });
test.KeyValues.Add("v2", new TestChild() { Value1 = "v2v1", Value2 = "v2v2" });
test.KeyValues.Add("v3", new TestChild() { Value1 = "v3v1", Value2 = "v3v2" });
test.KeyValues.Add("v4", null);

var seriliaze = System.Text.Json.JsonSerializer.Serialize(test).Dump();
System.Text.Json.JsonSerializer.Deserialize<TestObject>(seriliaze).Dump();

class TestChild
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
}

class TestObject
{
    public Dictionary<string, TestChild> KeyValues { get; set; }
    public string Name { get; set; }
}