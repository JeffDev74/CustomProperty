using UnityEngine;

namespace FPS
{
	public class ItemTests : MonoBehaviour
	{
        //public BuildblockItem BuildingBlock = new BuildblockItem();
        //public WeaponItem gun = new WeaponItem();

        public CampfireItem campfire = new CampfireItem();

        private void Start()
        {
            // Test create new custom string
            StringProperty MyCustomString = new StringProperty("campfire_ss", "I am a campfire.");
            (campfire.Data as WorldData).Properties.Add(MyCustomString);

            // Test get custom string value
            string description = (campfire.Data as WorldData).Properties.Get<string>("campfire_ss");
            Debug.Log("The saved value is [" + description + "]");

            // Test get non-existing key
            description = (campfire.Data as WorldData).Properties.Get<string>("none");
            Debug.Log("The saved value is [" + description + "]");

        }
    }
}