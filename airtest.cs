using System.Runtime.InteropServices;


        /// <summary>
        /// byte数组转结构体
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="type">结构体类型</param>
        /// <returns>转换后的结构体</returns>
        object BytesToStuct(byte[] bytes, Type type)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(type);
            //byte数组长度小于结构体的大小
            if (size > bytes.Length)
            {
                //返回空
                return null;
            }
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回结构体
            return obj;
        }

string val = "01 000.50bar       000.50L/min   000.51bar       000.21L/min   020 000 015 005 001 OK\r\n";
var bytes = Encoding.ASCII.GetBytes(val);
var obj = BytesToStuct(bytes, typeof(AirTestParams));

obj.Dump();
typeof(AirTestParams).GetFields().Dump();

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class AirTestParams
{
    /// <summary>
    /// 通道号
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
    public string Channel;
    public byte Empty1;
    /// <summary>
    /// 设定压力值
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
    public string SetPressValue;
    /// <summary>
    /// 设定压力值单位
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
    public string SetPressUnit;
    public byte Empty2;
    /// <summary>
    /// 设定漏量值
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
    public string SetLeakageValue;
    /// <summary>
    /// 设定漏量单位
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
    public string SetLeakageUnit;
    public byte Empty3;
    /// <summary>
    /// 实时压力
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
    public string RealTimePress;
    /// <summary>
    /// 实时压力单位
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
    public string RealTimePressUnit;
    public byte Empty4;
    /// <summary>
    /// 漏量结果
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
    public string LeakageResult;
    /// <summary>
    /// 漏量结果单位
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
    public string LeakageResultUnit;
    public byte Empty5;
    /// <summary>
    /// 充气时间
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
    public string InflationTime;
    public byte Empty6;
    /// <summary>
    /// 辅助充气时间
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
    public string AssitInflationTime;
    public byte Empty7;
    /// <summary>
    /// 平衡时间
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
    public string BalanceTime;
    public byte Empty8;
    /// <summary>
    /// 检测时间
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
    public string TestTime;
    public byte Empty9;
    /// <summary>
    /// 泄放时间
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
    public string ExhaustTime;
    public byte Empty10;
    /// <summary>
    /// 检测结果
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
    public string TestResult;
    /// <summary>
    /// 回车换行符
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
    public string Return;
}