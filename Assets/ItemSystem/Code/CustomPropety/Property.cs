using UnityEngine;

namespace FPS
{
    [System.Serializable]
    public class Property : IProperty
    {
        [SerializeField]
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [SerializeField]
        private string _key;
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        [SerializeField]
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Property(string key, object value)
        {
            this.Type = value.GetType().ToString();
            this.Key = key;
            this.Value = value.ToString();
        }

        public virtual string Serialize(object obj) { return obj.ToString(); }

        public virtual T Deserialize<T>() { return default(T); }
    }
}