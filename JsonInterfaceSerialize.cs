#r "nuget: Newtonsoft.Json, 13.0.4"

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

ITestRecord record1 = new Record1()
{
    PublicInt = 1
};

ITestRecord record2 = new Record2()
{
    OtherInt = 2,
    PublicInt = 2
};

JsonConvert.SerializeObject(record1).Dump();
JsonConvert.SerializeObject(record2).Dump();

interface ITestRecord
{
    int PublicInt{get;}
}

class Record1 : ITestRecord
{
    public int PublicInt { get; set; }
}

class Record2 : ITestRecord
{
    public int OtherInt { get; set; }
    public int PublicInt { get; set; }
}