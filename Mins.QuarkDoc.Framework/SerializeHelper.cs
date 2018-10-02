using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace Mins.QuarkDoc.Framework
{
    public class SerializeHelper
    {  
        public static string JSONSerialize(object obj)
        {
            JavaScriptSerializer javaScriptSerialiser = new JavaScriptSerializer();
            return javaScriptSerialiser.Serialize(obj);
        }

        public static T JSONDeserialize<T>(string jsonDatas)
        {
            if (string.IsNullOrEmpty(jsonDatas))
                return default(T);

            return new JavaScriptSerializer().Deserialize<T>(jsonDatas);
        }

        public static string GetJsonValue(string jsonDatas, string key)
        {
            JObject jo = JObject.Parse(jsonDatas);
            return (string)jo[key];
        }

    }
}
