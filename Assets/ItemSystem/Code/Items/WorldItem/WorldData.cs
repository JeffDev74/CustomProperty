using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS
{
    [System.Serializable]
    public class WorldData : BaseData, ISerializeData
    {
        [SerializeField]
        public PropertyManager Properties;

        #region ISerializeData Implementation
        public string SerializeItemData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            using (memoryStream)
            {
                formatter.Serialize(memoryStream, this);
            }
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public BaseItem FactoryCloneItemFromData()
        {
            string originalUUID = itemUUID;

            CampfireItem factoredItem = new CampfireItem(this);

            factoredItem.Data.itemUUID = originalUUID;

            return factoredItem;
        }
        #endregion ISerializeData Implementation
    }
}