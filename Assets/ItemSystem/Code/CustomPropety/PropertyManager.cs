using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS
{
    [System.Serializable]
	public class PropertyManager : IPropertySerializer
    {
        [SerializeField]
        public List<Property> _props;
        public List<Property> Props
        {
            get
            {
                if(_props == null)
                {
                    _props = new List<Property>();
                }
                return _props;
            }
            private set { _props = value; }
        }

        public T Get<T>(string key)
        {
            foreach (Property p in Props)
            {
                if(p.Key == key)
                {
                    IProperty pInterface = p as IProperty;
                    if (pInterface != null)
                    {
                        return pInterface.Deserialize<T>();
                    }
                    else
                    {
                        Debug.LogWarning("[GetProp] The custom property [" + key + "] does not implement the IProperty");
                        return default(T);
                    }
                }
            }

            Debug.LogWarning("[Get] The custom property [" + key + "] was not found. To create a property use Add() method.");
            return default(T);
        }

        public void Set(string key, object value)
        {
            foreach (Property p in Props)
            {
                if (p.Key == key)
                {
                    p.Type = value.GetType().ToString();
                    p.Value = value.ToString();
                    return;
                }
            }
            Debug.LogWarning("[Set] The custom property [" + key + "] was not found. If you want to create it use Add() method.");
        }

        public void Add(Property prop)
        {
            // Check if we have this property already
            foreach (Property p in Props)
            {
                if (p.Key == prop.Key)
                {
                    Debug.LogWarning("[Add] The custom property with key of [" + prop.Key + "] already exists. If you want to update its value use the Set() method.");
                    return;
                }
            }
            Debug.Log("[Add] Adding property with key of [" + prop.Key + "]");
            // The property was not found lets create it
            Props.Add(prop);
        }

        public void Delete(string key)
        {
            foreach (Property p in Props.ToArray())
            {
                if (p.Key == key)
                {
                    Props.Remove(p);
                    return;
                }
            }
            Debug.LogWarning("[Delete] The custom property with key of [" + key + "] does not exist.");
        }

        public string Serialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            using (memoryStream)
            {
                formatter.Serialize(memoryStream, _props);
            }
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public void Deserialize<D>(string data) where D : List<Property>
        {
            try
            {
                Stream stream = DecodeItem(data);
                BinaryFormatter formatter = new BinaryFormatter();
                D newData = formatter.Deserialize(stream) as D;
                Props = newData;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError(e.StackTrace);
                throw new Exception("Failed to factore custom properties.");
                //return null;
            }
        }

        public static Stream DecodeItem(string data)
        {
            try
            {
                byte[] byteArray = System.Convert.FromBase64String(data);
                var stream = new MemoryStream(byteArray);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                Debug.LogError(ex.StackTrace);
                throw new System.Exception("Failed to decode item data.");
            }
        }
    }
}