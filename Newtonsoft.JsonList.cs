#r "nuget: Newtonsoft.Json, 13.0.3"
using Newtonsoft.Json;

var test = JsonConvert.DeserializeObject<JsonTest>("{\"Ints\":[1,2,3]}", 
    new JsonSerializerSettings()
    {
        ObjectCreationHandling = ObjectCreationHandling.Replace
    }
);
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