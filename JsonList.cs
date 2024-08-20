using System.Text.Json;

var test = JsonSerializer.Deserialize<JsonTest>("{\"Ints\":[1,2,3]}");
test.Dump();

public class JsonTest 
{
    public List<int> Ints {get;set;}
    public JsonTest()
    {
        Ints = new()
        {
            3,2,1
        };
    }
}