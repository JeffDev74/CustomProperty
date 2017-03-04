using UnityEngine;

namespace FPS
{
    [System.Serializable]
	public class WeaponItem : BaseItem, IBaseData
	{
        [SerializeField]
        private WeaponBaseData _data;
        public override BaseData Data
        {
            get { return _data; }
            set { _data = value as WeaponBaseData; }
        }

        [SerializeField]
        private WeaponNSData _nsData;
        public override BaseNSData NSData
        {
            get { return _nsData; }
            set { _nsData = value as WeaponNSData; }
        }
    }
}