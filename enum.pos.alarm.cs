string AlarmErrors(Type enumType, int Content)
{
    string result = "";
    var str = Convert.ToString(Content, 2);
    var charArray = str.ToCharArray();

    for (int i = 0; i < charArray.Length; i++)
    {
        var value = charArray[charArray.Length - (i + 1)] == '1';
        if (value)
        {
            //if (enumType == typeof(EnumAlarmError5))
            //{
            //    var enumI = Enum.ToObject(enumType, (i + 1) + 8);
            //    result += enumI.ToString() + ";";
            //}
            //else
            //{
            var enumI = Enum.ToObject(enumType, (i + 1));
            result += enumI.ToString() + ";";
            //  }

        }
    }

    return result;
}

string.IsNullOrEmpty(AlarmErrors(typeof(TestEnum), 0)).Dump();

enum TestEnum
{
    None = 0,
    Alarm1,
    Alarm2,
    Alarm3,
    Alarm4,
    Alarm5,
    Alarm6,
    Alarm7,
    Alarm8,
}