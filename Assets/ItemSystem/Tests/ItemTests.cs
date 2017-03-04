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
            StringProperty MyCustomString = new StringProperty("campfire_ss", "I am a campfire.");
            (campfire.Data as WorldData).Properties.AddProp(MyCustomString);

            string description = (campfire.Data as WorldData).Properties.GetProp<string>("campfire_ss");

            Debug.Log("The saved value is [" + description + "]");


        }
    }
}