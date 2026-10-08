#r "nuget: Newtonsoft.Json, 13.0.4"

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var json = "[{\"Id\": 1, \"Record\": \"Record1\"}, {\"Id\": 2, \"Record\": \"Record2\"}]";
var json2 = "{\"Id\": 3, \"Record\": \"Record3\"}";
var token = JToken.Parse(json);
token.Select(jt => jt.ToObject<RecordTest>()).Dump();

var token2 = JToken.Parse(json2);
token2.ToObject<OtherRecordTest>().Dump();

public record RecordTest(int Id, string Record);
public record OtherRecordTest(int OtherId, string OtherRecord);