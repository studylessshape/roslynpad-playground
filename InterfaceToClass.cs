using System.Collections.Generic;
using System.Linq;
using System;

var list = new List<ITestIn>();
for(int i = 0; i < 10;i ++)
{
    list.Add(new TestClass(1, $"v{i}", $"o{i}"));
}

var other = list.ToArray().Select(v => (TestClass)v).ToArray();

foreach(var item in other)
{
    Console.WriteLine($"ValueInt: {item.ValueInt},ValueString: {item.ValueString},OutClassValue: {item.OutClassValue}");
}

interface ITestIn
{
    int ValueInt { get; set; }
    string ValueString { get; set; }
}

class TestClass : ITestIn
{
    public int ValueInt { get; set; }
    public string ValueString { get; set; }
    public string OutClassValue { get; set; }

    public TestClass(int valueInt, string valueString, string outClassValue)
    {
        ValueInt = valueInt;
        ValueString = valueString;
        OutClassValue = outClassValue;
    }
}