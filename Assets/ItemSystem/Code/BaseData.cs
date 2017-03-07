using UnityEngine;

namespace FPS
{
    [System.Serializable]
	public class BaseData
	{
        [SerializeField]
        public int ID;
        [SerializeField]
        public string itemUUID;
        [SerializeField]
        public string ItemName;
        [SerializeField]
        public ItemTypeEnum Type;

        [SerializeField]
        public PropertyManager Properties;
    }
}