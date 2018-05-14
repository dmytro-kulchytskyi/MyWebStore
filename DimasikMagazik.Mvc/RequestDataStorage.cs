using DimasikMagazik.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace DimasikMagazik.Mvc
{
    public class RequestDataStorage : IRequestDataStorage
    {
        private Dictionary<string, object> dictionary;

        public RequestDataStorage()
        {
            var dict = (Dictionary<string, object>)CallContext.GetData("RequestDataStorage");

            if (dict == null)
            {
                dict = new Dictionary<string, object>();
                CallContext.SetData("RequestDataStorage", dict);
            }

            dictionary = dict;
        }

        public T GetValue<T>(string key)
        {
            if (dictionary.ContainsKey(key))
                return (T)dictionary[key];

            return default(T);
        }

        public void SetValue<T>(string key, T value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }
    }
}