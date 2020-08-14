using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace DotNetAutoUpdater
{
    public static class XmlSerializerHelper
    {
        private static Dictionary<Type, XmlSerializer> _cache = new Dictionary<Type, XmlSerializer>();

        private static object SyncRootCache = new object();

        public static XmlSerializer Create(Type type)
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
    }
}