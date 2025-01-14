#r "nuget: Newtonsoft.Json, 13.0.3"

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var obj = new TestObject<xy>() {x = "12", data = new xy() {x = "1"}};

var force = (TestObject)obj;

var serialize= JsonConvert.SerializeObject(obj).Dump();
var de = JsonConvert.DeserializeObject<TestObject>(serialize).Dump();

if (de.data is JObject jobj)
{
    jobj.ToObject<xy>().Dump();
}

class xy
{
    public string? x {get;set;}
    public string? y {get;set;}
}

class TestObject : TestObject<object>
{
}

class TestObject<T>
{
    public string x {get;set;}
    public T data {get;set;}
}