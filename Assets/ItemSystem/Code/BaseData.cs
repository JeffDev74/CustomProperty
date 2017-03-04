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
        //public string itemUUID
        //{
        //    get
        //    {
        //        if(string.IsNullOrEmpty(_itemUUID))
        //        {
        //            _itemUUID = System.Guid.NewGuid().ToString();
        //        }
        //        return _itemUUID;
        //    }
        //    set { _itemUUID = value; }
        //}
        [SerializeField]
        public string ItemName;
        [SerializeField]
        public ItemTypeEnum Type;
    }
}