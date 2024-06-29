string GetValueString<T>(T val)
    where T: Enum
{
    return val.GetHashCode().ToString();    
}

GetValueString(TestEnum.Key2).Dump();

enum TestEnum
{
    Key1 = 1,
    Key2,
    Key3,
}