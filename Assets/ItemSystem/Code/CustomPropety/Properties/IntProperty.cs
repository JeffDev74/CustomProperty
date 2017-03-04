using UnityEngine;

namespace FPS
{
	public class IntProperty : Property, IProperty
    {
        public IntProperty(string key, object value) : base(key, value) { }

        public override T Deserialize<T>()
        {
            return (T)System.Convert.ChangeType(Value, typeof(T));
        }

        public override string Serialize(object obj)
        {
            return obj.ToString();
        }
    }
}