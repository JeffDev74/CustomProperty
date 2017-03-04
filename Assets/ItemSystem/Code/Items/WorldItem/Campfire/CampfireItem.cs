using UnityEngine;

namespace FPS
{
    [System.Serializable]
	public class CampfireItem : BaseItem, IBaseData
	{
        public CampfireItem (WorldData data) : base()
        {
            Data = data;
            Init();
        }

        public CampfireItem()
        {
            Data = new WorldData();
            NSData = new WorldNSData();
            Init();
        }

        private void Init()
        {
            Data.Type = ItemTypeEnum.WorldItem;
        }

        [SerializeField]
        private WorldData _data;
        public override BaseData Data
        {
            get { return _data; }
            set { _data = value as WorldData; }
        }

        [SerializeField]
        private WorldNSData _nsData;
        public override BaseNSData NSData
        {
            get { return _nsData; }
            set { _nsData = value as WorldNSData; }
        }
    }
}