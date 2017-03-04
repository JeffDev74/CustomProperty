using System;
using UnityEngine;

namespace FPS
{
    [System.Serializable]
    public class BuildblockItem : BaseItem, IBaseData
    {
        [SerializeField]
        private WorldData _data;
        public BaseData Data
        {
            get { return _data; }
            set { _data = value as WorldData; }
        }

        [SerializeField]
        private WorldNSData _nsData;
        public BaseNSData NSData
        {
            get { return _nsData; }
            set { _nsData = value as WorldNSData; }
        }
    }
}