#r "nuget: Newtonsoft.Json.Schema, 4.0.1"
#r "nuget: Newtonsoft.Json, 13.0.3"

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

JSchemaGenerator generator = new JSchemaGenerator();
var schema = generator.Generate(typeof(RoboticJointRuler));
schema.Dump();

public enum RoboticRulerThresholdType
{
    InThreshold,
    OutThreshold,
}

public enum RoboticOutThresholdResult
{
    /// <summary>
    /// 临界点
    /// </summary>
    TippingPoint,
    /// <summary>
    /// 奇异点
    /// </summary>
    Singularity
}

public enum MatchType
{
    /// <summary>
    /// Only Start
    /// </summary>
    Start = 1 << 0,
    /// <summary>
    /// Exclude Start and End
    /// </summary>
    Anywhere = 1 << 1,
    /// <summary>
    /// Only End
    /// </summary>
    End = 1 << 2,
    /// <summary>
    /// Match Start and End
    /// </summary>
    StartEnd = Start | End,
    /// <summary>
    /// Match All
    /// </summary>
    All = Start | Anywhere | End,
}

public class Threshold<T>
    where T : IComparable<T>
{
    public T Maximum { get; set; }
    public T Minimum { get; set; }
    public bool ExcludeMaximum { get; set; }
    public bool ExcludeMinimum { get; set; }

    public bool InThreshold(T value)
    {
        var compareToMinimum = value.CompareTo(Minimum);
        if ((ExcludeMinimum && compareToMinimum == 0) || compareToMinimum < 0)
        {
            return false;
        }

        var compareToMaximum = value.CompareTo(Maximum);
        if ((ExcludeMaximum && compareToMaximum == 0) || compareToMaximum > 0)
        {
            return false;
        }

        return true;
    }
}

public class SingleRuler
    {
        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        [JsonConverter(typeof(StringEnumConverter))]
        public RoboticRulerThresholdType ThresholdType { get; set; } = RoboticRulerThresholdType.InThreshold;
        [JsonConverter(typeof(StringEnumConverter))]
        public RoboticOutThresholdResult OutThresholdResult { get; set; } = RoboticOutThresholdResult.TippingPoint;
        public List<Threshold<double>> Threshold { get; set; }
    }

    public class RulerGroup
    {
        public required string Name { get; set; }
        public List<SingleRuler> Rulers { get; set; }
    }

    public class JointRuler
    {
        public required int JointIndex { get; set; }
        public required string RulerName { get; set; }
    }

public class RoboticJointRuler
{
    public required string Controller { get; set; }
    public bool ControllerMatch { get; set; } = true;
    [JsonConverter(typeof(StringEnumConverter))]
    public MatchType ControllerMatchType { get; set; } = MatchType.Start;
    public List<RulerGroup> DefaineRulers { get; set; }
    public List<JointRuler> JointRulers { get; set; }
}