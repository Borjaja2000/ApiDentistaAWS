using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace ApiDentistaAWS.Helper
{
    public class HelperToolKit
    {
        public static string SanitizeFillName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }

        public static bool ArraysComparative(byte[] a, byte[] b)
        {
            bool equals = true;
            if (a.Length != b.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].Equals(b[i]) == false)
                    {
                        equals = false;
                        break;
                    }
                }
            }
            return equals;
        }

        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return obj;
            }
        }

        //METODO QUE RECIBIRA UN OBJETO Y LO TRANSFORMARA
        //EN String Json
        public static String SerializeJsonObject(object objeto)
        {
            String respuesta =
                JsonConvert.SerializeObject(objeto);
            return respuesta;
        }

        //METODO QUE RECIBIRA UN String Json Y DEVOLVERA EL OBJETO
        //QUE REPRESENTA DICHO JSON
        public static T DeserializeJsonObject<T>(String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
