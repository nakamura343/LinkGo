using System.IO;
using System.Xml.Serialization;

namespace LinkGo.Base.Utils
{
    /// <summary>  
    /// <remarks>Xml序列化与反序列化</remarks>  
    /// <creator>zhangdapeng</creator>  
    /// </summary>  
    public class XmlSerializeUtil
    {
        #region 反序列化   
        public static T Deserialize<T>(string xml)
        {
            T value = default(T);

            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(typeof(T));
                value = (T)xmldes.Deserialize(sr);
            }

            return value;
        }

        public static T DeserializeXml<T>(byte[] bytes)
        {
            T value = default(T);

            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (TextReader textReader = new StreamReader(memoryStream))
                {
                    value = (T)deserializer.Deserialize(textReader);
                }
            }

            return value;
        }

        public static T Deserialize<T>(Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            return (T)xmldes.Deserialize(stream);
        }

        public static T DeserializeFromFile<T>(string path)
        {
            if (!File.Exists(path))
            {
                return default(T);
            }

            string text = File.ReadAllText(path);

            return Deserialize<T>(text);
        }
        #endregion

        #region 序列化  
        public static string Serializer<T>(T obj)
        {
            string str;

            XmlSerializer xml = new XmlSerializer(typeof(T));
            using (MemoryStream Stream = new MemoryStream())
            {
                //序列化对象  
                xml.Serialize(Stream, obj);
                Stream.Position = 0;
                using (StreamReader sr = new StreamReader(Stream))
                {
                    str = sr.ReadToEnd();
                }
            }

            return str;
        }

        public static void SerializeToFile(string path, object o)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string contents = Serializer(o);
            if (contents != null)
            {
                File.WriteAllText(path, contents);
            }
        }
        #endregion
    }
}
