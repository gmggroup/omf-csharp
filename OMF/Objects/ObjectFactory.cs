using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OMF.Objects
{
    public static class ObjectFactory
    {
        public class TypeSingleton
        {
            private static TypeSingleton instance;

            private TypeSingleton() { }

            private Dictionary<string, Type> m_ObjectTypes;

            public Dictionary<string, Type> ObjectTypes
            {
                get
                {
                    return m_ObjectTypes;
                }
            }
            public Type GetObjectType(string className)
            {
                if(m_ObjectTypes.ContainsKey(className))
                {
                    return m_ObjectTypes[className];
                }
                else
                {
                    return null;
                }
            }
            public static TypeSingleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new TypeSingleton();
                        instance.m_ObjectTypes = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

                        foreach (Type mytype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(IObject))))
                        {
                            instance.m_ObjectTypes.Add(mytype.Name, mytype);
                        }
                    }
                    return instance;
                }
            }
        }

        public static List<OMF.Objects.IObject> DeserializeObjects(Dictionary<string, object> jsonDict, BinaryReader br,string[] guids)
        {
            if (guids != null)
            {
                List<OMF.Objects.IObject> Objects = new List<OMF.Objects.IObject>();

                foreach (string guid in guids)
                {
                    IObject toadd = ObjectFactory.GetObjectFromGuid(jsonDict, br, guid);
                    if (toadd != null)
                    {
                        Objects.Add(toadd);
                    }
                }

                return Objects;
            }
            return null;
        }
        public static IObject GetObjectFromData(Dictionary<string, object> jsonDict, BinaryReader br, string data)
        {

            Dictionary<string, object> thisDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (thisDict.ContainsKey("__class__"))
            {
                string classname = thisDict["__class__"].ToString();

                Type t = TypeSingleton.Instance.GetObjectType(classname);

                if (t != null)
                {
                    IObject returnvalue = (IObject)JsonConvert.DeserializeObject(data, t);
                    if (returnvalue != null)
                    {
                        returnvalue.Deserialize(jsonDict, br);
                        return returnvalue;
                    }
                }
            }


            return null;
        }
        public static IObject GetObjectFromGuid(Dictionary<string, object> jsonDict, BinaryReader br, string guid)
        {
            if (jsonDict.ContainsKey(guid))
            {
                string data = jsonDict[guid].ToString();

                return GetObjectFromData(jsonDict, br, data);
            }
            else
            {
                return null;
            }
        }
    }
}
