using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    [System.Serializable]
	public class PropertyManager
	{
        public List<Property> props;

        public T GetProp<T>(string key)
        {
            foreach (Property p in props)
            {
                IProperty pInterface = p as IProperty;
                if (pInterface != null)
                {
                    if (pInterface.Key == key)
                    {
                        return pInterface.Deserialize<T>();
                    }
                }
                else
                {
                    Debug.LogWarning("[GetProp] The custom property [" + key + "] was not found. If you want to create it use AddProp() method.");
                    return default(T);
                }
            }

            Debug.LogWarning("[GetProp] The custom property [" + key + "] Does not implement the IProperty interface.");
            return default(T);
        }

        public void SetProp(string key, object value)
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
            Debug.LogWarning("[SetProp] The custom property [" + key + "] was not found. If you want to create it use AddProp() method.");
        }

        public void AddProp(Property prop)
        {
            // Check if we have this property already
            foreach (Property p in props)
            {
                if (p.Key == prop.Key)
                {
                    Debug.LogWarning("[AddProp] The custom property with key of [" + prop.Key + "] already exists. If you want to update its value use the SetProp() method.");
                    return;
                }
            }
            Debug.Log("[AddProp] Adding property with key of [" + prop.Key + "]");
            // The property was not found lets create it
            props.Add(prop);
        }

        public void DeleteProp(string key)
        {
            foreach (Property p in props.ToArray())
            {
                if (p.Key == key)
                {
                    props.Remove(p);
                    return;
                }
            }
            Debug.LogWarning("[DeleteProp] The custom property with key of [" + key + "] does not exist.");
        }
    }
    
}