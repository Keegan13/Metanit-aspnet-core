using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System
{
    public static class SerializationTool
    {
        public static void Set<T>(this Microsoft.AspNetCore.Http.ISession session,string key ,T value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));
        }
        public static T Get<T>(this Microsoft.AspNetCore.Http.ISession session, string key)
        {
            string JsonObj=session.GetString(key);
            return JsonObj == null ? default(T) : JsonConvert.DeserializeObject<T>(JsonObj);
        }
    }
}
