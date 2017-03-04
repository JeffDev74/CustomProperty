using UnityEngine;

namespace FPS
{
    [System.Serializable]
	public class BaseData
	{
        public int ID;
        public string itemUUID;
        public string ItemName;
        public ItemTypeEnum Type;
    }
}