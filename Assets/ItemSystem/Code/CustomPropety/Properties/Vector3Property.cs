using UnityEngine;

namespace FPS
{
    [System.Serializable]
    public class Vector3Property : Property, IProperty
    {
        public Vector3Property(string key, object value) : base(key, value) { }

        public override T Deserialize<T>()
        {
            string svalue = "";
            // Remove the parentheses
            if (Value.StartsWith("(") && Value.EndsWith(")"))
            {
                svalue = Value.Substring(1, Value.Length - 2);
            }

            // split the items
            string[] sArray = svalue.Split(',');

            // store as a Vector3
            Vector3 result = new Vector3(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]));

            return (T)System.Convert.ChangeType(result, typeof(T));
        }

        public override string Serialize(object obj)
        {
            return obj.ToString();
        }
    }
}