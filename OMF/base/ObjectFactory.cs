﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OMF.Base
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
                if (m_ObjectTypes.ContainsKey(className))
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

        public static List<OMF.Base.IObject> DeserializeObjects(Dictionary<string, object> jsonDict, BinaryReader br, string[] guids)
        {
            if (guids != null)
            {
                List<OMF.Base.IObject> Objects = new List<OMF.Base.IObject>();

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
        public static void GetObjectToData(Dictionary<string, object> jsonDict, IObject toSerialize,string guid)
        {
            string jsonString = JsonConvert.SerializeObject(toSerialize);
            jsonDict.Add(guid, jsonString);
        }
        public static string SerializeObject(IObject objs, Dictionary<string, object> jsonDict, BinaryWriter bw)
        {
            
            if(objs==null)
            {
                return "";
            }
            else
            {
                string guid = Guid.NewGuid().ToString();
                objs.Serialize(jsonDict, bw, guid);
                return guid;
            }
        }
        public static string[] SerializeObjects(List<IObject> objs, Dictionary<string, object> jsonDict, BinaryWriter bw)
        {
            if(objs==null)
            {
                return null;
            }
            List<string> guids = new List<string>();
            for (int i = 0; i < objs.Count; i++)
            {
                string thisGuid = Guid.NewGuid().ToString();
                guids.Add(thisGuid);
                objs[i].Serialize(jsonDict, bw, thisGuid);
            }
            return guids.ToArray();
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
            if(string.IsNullOrEmpty(guid))
            {
                return null;
            }
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