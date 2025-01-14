#r "nuget: Newtonsoft.Json, 13.0.3"

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var response = ErpApiReturn.Ok("123", "123");
JsonConvert.SerializeObject(response).Dump();

public class ErpApiReturn<T> : ICloneable
{
    /// <summary>
    /// 错误码为0:表示成功
    /// </summary>
    public int? returnCode { get; set; }
    /// <summary>
    /// 错误码不等于0：会有错误信息描述，英文描述
    /// </summary>
    public string returnMsg { get; set; }
    /// <summary>
    /// 错误码不等于0：会有错误信息描述，中文描述
    /// </summary>
    public string returnUserMsg { get; set; }
    /// <summary>
    /// 根据接口情况，如果需要同步返回数据，则填充至此字段
    /// </summary>
    public T data { get; set; }

    public int? code { get; set; }
    public string msg { get; set; }
    public bool? success { get; set; }

    public bool IsOk()
    {
        return code == 200 || ((returnCode == 0 || returnCode == 200) && (success == null || success == true));
    }

    [JsonIgnore]
    public string Msg
    {
        get
        {
            if (msg != null)
            {
                return msg;
            }
            else if (returnUserMsg != null)
            {
                return returnUserMsg;
            }
            else
            {
                return returnMsg;
            }
        }
    }
    
    public static ErpApiReturn<T> Ok(T data, string returnMsg = null, string reutrnUserMsg = null)
    {
        return new ErpApiReturn<T>()
        {
            returnCode = 0,
            data = data,
            returnMsg = returnMsg ?? "success",
            returnUserMsg = reutrnUserMsg ?? "成功",
        };
    }

    public object Clone()
    {
        return new ErpApiReturn<T>()
        {
            returnCode = this.returnCode,
            returnMsg = (string)this.returnMsg?.Clone(),
            returnUserMsg = (string)this.returnUserMsg?.Clone(),
            data = this.data,
            code = this.code,
            msg = (string)this.msg.Clone(),
        };
    }

    /// <summary>
    /// 可能会报错
    /// </summary>
    /// <param name="return"></param>
    public static explicit operator ErpApiReturn<T>(ErpApiReturn @return)
    {
        return new ErpApiReturn<T>()
        {
            data = @return.DeserializeData<T>(false),
            returnCode = @return.returnCode,
            returnMsg = @return.returnMsg,
            returnUserMsg = @return.returnUserMsg,
        };
    }
}

/// <summary>
/// 默认没有数据的返回对象
/// </summary>
public class ErpApiReturn : ErpApiReturn<object>
{
    public ErpApiReturn()
    {
        data = new object();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="record">是否需要将解析的数据保存到</param>
    /// <returns></returns>
    public T DeserializeData<T>(bool record = false)
    {
        if (this.data is T d)
        {
            return d;
        }

        if (typeof(T) == typeof(string))
        {
            var sd = (T)(object)this.data?.ToString();
            if (record)
            {
                this.data = sd;
            }
            return sd;
        }

        if (this.data is string str)
        {
            // json
            var jsonData = JsonConvert.DeserializeObject<T>(str);

            if (record)
            {
                this.data = jsonData;
            }

            return jsonData;
        }
        else if (this.data is JObject jobj)
        {
            var jData = jobj.ToObject<T>();
            this.data = jData;

            return jData;
        }
        else
        {
            T force = (T)this.data;
            this.data = force;
            return force;
        }
    }

    /// <summary>
    /// <para>尝试获取 Data 数据，如果 record 设置为 true，内部的 Data 会在获取后，变为获取后的 Data</para>
    /// <para>如果启用record，会导致在多次获取不同的数据类型之后，</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="record"></param>
    /// <returns></returns>
    public bool TryDeserializeData<T>(out T data, bool record = false)
    {
        try
        {
            data = DeserializeData<T>(record);
            return true;
        }
        catch (System.Exception)
        {
            data = default;
            return false;
        }
    }

    public static implicit operator ErpApiReturn(ErpApiReturn<dynamic> @return)
    {
        return From(@return);
    }

    public static ErpApiReturn From<T>(ErpApiReturn<T> other)
    {
        return new ErpApiReturn()
        {
            returnCode = other.returnCode,
            returnMsg = other.returnMsg,
            returnUserMsg = other.returnUserMsg,
            data = other.data,
            code = other.code,
            msg = other.msg
        };
    }

    public static ErpApiReturn Ok(string returnMsg = null, string reutrnUserMsg = null)
    {
        return new ErpApiReturn()
        {
            returnCode = 0,
            data = new object(),
            returnMsg = returnMsg,
            returnUserMsg = reutrnUserMsg,
        };
    }

    public static ErpApiReturn<T> Ok<T>(T data, string returnMsg = null, string reutrnUserMsg = null)
    {
        return new ErpApiReturn<T>()
        {
            returnCode = 0,
            data = data,
            returnMsg = returnMsg,
            returnUserMsg = reutrnUserMsg,
        };
    }
}