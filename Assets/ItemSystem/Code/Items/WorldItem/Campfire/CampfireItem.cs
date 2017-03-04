using UnityEngine;

namespace FPS
{
    [System.Serializable]
	public class CampfireItem : BaseItem, IBaseData
	{
        public CampfireItem (WorldData data) : base()
        {
            Data = data;
        }

        public CampfireItem()
        {
            Data = new WorldData();
        }

        [SerializeField]
        private WorldData _data;
        public new BaseData Data
        {
            get { return _data; }
            set { _data = value as WorldData; }
        }

        [SerializeField]
        private WorldNSData _nsData;
        public new BaseNSData NSData
        {
            get { return _nsData; }
            set { _nsData = value as WorldNSData; }
        }
    }
}