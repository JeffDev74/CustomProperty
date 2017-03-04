using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    [System.Serializable]
	public class PropertyManager
	{
        public List<Property> props;

        public T Get<T>(string key)
        {
            foreach (Property p in props)
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
            foreach (Property p in props)
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
            foreach (Property p in props)
            {
                if (p.Key == prop.Key)
                {
                    Debug.LogWarning("[Add] The custom property with key of [" + prop.Key + "] already exists. If you want to update its value use the Set() method.");
                    return;
                }
            }
            Debug.Log("[Add] Adding property with key of [" + prop.Key + "]");
            // The property was not found lets create it
            props.Add(prop);
        }

        public void Delete(string key)
        {
            foreach (Property p in props.ToArray())
            {
                if (p.Key == key)
                {
                    props.Remove(p);
                    return;
                }
            }
            Debug.LogWarning("[Delete] The custom property with key of [" + key + "] does not exist.");
        }
    }
    
}