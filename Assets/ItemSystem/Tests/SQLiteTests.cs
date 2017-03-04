using UnityEngine;

namespace FPS
{
	public class SQLiteTests : MonoBehaviour
	{
        SQLiteItemSerializer _dbModel;
        SQLiteItemSerializer DBModel
        {
            get
            {
                if(_dbModel == null)
                {
                    _dbModel = SQLiteItemSerializer.instance;
                    //_dbModel = GetComponent<SQLiteItemSerializer>();
                }
                return _dbModel;
            }
        }

        public CampfireItem campfire;

        private void Start()
        {
            //TestGetAllItems();
            //TestSaveItemCustomProperty();
            TestItemWithCustomProperties();
        }

        public void TestGetAllItems()
        {
            BaseItem[] items = DBModel.GetAllItems();
            Debug.Log("[TestGetAllItems] We have [" + items.Length + "] in the sqlite database.");
            for (int i = 0; i < items.Length; i++)
            {
                Debug.Log("[TestGetAllItems] Got item [" + items[i].Data.ItemName + "] with id of [" + items[i].Data.itemUUID + "]");
            }
        }

        public void TestItemWithCustomProperties()
        {
            BaseItem[] items = DBModel.GetAllItems();
            Debug.Log("[TestItemWithCustomProperties] We have [" + items.Length + "] in the sqlite database.");
            for (int i = 0; i < items.Length; i++)
            {
                BaseItem item = items[i];

                Debug.Log("[TestItemWithCustomProperties] Got item [" + item.Data.ItemName + "] with id of [" + item.Data.itemUUID + "]");

                string description = item.Data.Properties.Get<string>("description");
                Vector3 position = item.Data.Properties.Get<Vector3>("position");

                Debug.Log("[TestItemWithCustomProperties] custom string is [" + description + "] custom vector is [" + position + "]");
            }
        }

        public void TestCreateItem()
        {
            campfire.Data.ItemName = "Super Campfire";
            campfire.Data.itemUUID = System.Guid.NewGuid().ToString();

            IBaseData dataInterface = campfire as IBaseData;
            if (dataInterface != null)
            {
                DBModel.CreateItem(dataInterface);
                Debug.Log("[TestCreateItem] The item was successfully created.");
            }
            else
            {
                Debug.LogError("[TestCreateItem] The item data does not implement the IBaseData interface.");
            }
        }

        public void TestSaveItemCustomProperty()
        {
            campfire = new CampfireItem();
            campfire.Data.ItemName = "Campfire Custom Props";
            campfire.Data.itemUUID = System.Guid.NewGuid().ToString();

            // Test add string custom property
            StringProperty MyDescriptionProperty = new StringProperty("description", "Campfire really warm :)");
            campfire.Data.Properties.Add(MyDescriptionProperty);

            // string description = (campfire.Data as WorldData).Properties.Get<string>("description");
            // Debug.Log("The saved value is [" + description + "]");

            // Test add vector3 custom property
            Vector3Property MyVector3Property = new Vector3Property("position", new Vector3(11, 22, 33));
            campfire.Data.Properties.Add(MyVector3Property);

            IBaseData dataInterface = campfire as IBaseData;
            if (dataInterface != null)
            {
                DBModel.CreateItem(dataInterface);
                Debug.Log("[TestSaveItemCustomProperty] The item with custom properties was successfully created.");
            }
            else
            {
                Debug.LogError("[TestSaveItemCustomProperty] The item with custom properties data does not implement the IBaseData interface.");
            }
        }
    }
}