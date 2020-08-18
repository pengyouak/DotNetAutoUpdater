using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace DotNetAutoUpdater
{
    public static class XmlSerializerHelper
    {
        private static Dictionary<Type, XmlSerializer> _cache = new Dictionary<Type, XmlSerializer>();

        private static object SyncRootCache = new object();

        private static XmlSerializer Create(Type type)
        {
            XmlSerializer serializer;

            lock (SyncRootCache)
            {
                if (_cache.TryGetValue(type, out serializer))
                    return serializer;
            }

            lock (type)
            {
                lock (SyncRootCache)
                {
                    if (_cache.TryGetValue(type, out serializer))
                        return serializer;
                }
                serializer = XmlSerializer.FromTypes(new[] { type }).FirstOrDefault();
                lock (SyncRootCache)
                {
                    if (serializer != null) _cache[type] = serializer;
                }
            }
            return serializer;
        }

        public static T XmlDeSerializeObject<T>(string str)
        {
            var xs = Create(typeof(T));
            var reader = new XmlTextReader(new StringReader(str));
            var config = (T)xs.Deserialize(reader);
            reader.Close();
            return config;
        }

        public static void XmlSerializeObject<T>(T updateOption, string file)
        {
            var xs = Create(typeof(T));
            using (var writer = XmlWriter.Create(file))
                xs.Serialize(writer, updateOption);
        }
    }
}