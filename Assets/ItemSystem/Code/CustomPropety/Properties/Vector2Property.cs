using UnityEngine;

namespace FPS
{
    [System.Serializable]
    public class Vector2Property : Property, IProperty
    {
        public Vector2Property(string key, object value) : base(key, value) { }

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

            // store as a Vector2
            Vector2 result = new Vector2(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]));

            return (T)System.Convert.ChangeType(result, typeof(T));
        }

        public override string Serialize(object obj)
        {
            return obj.ToString();
        }
    }
}