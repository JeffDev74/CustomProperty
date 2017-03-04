using UnityEngine;

namespace FPS
{
	public class CustomoPropertyTests : MonoBehaviour
	{
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


            // Add Int property
            IntProperty MyInt = new IntProperty("our_int", 56);
            (campfire.Data as WorldData).Properties.Add(MyInt);

            // Test and test the update of the int property
            int testint = (campfire.Data as WorldData).Properties.Get<int>("our_int");
            Debug.Log("The Myint value is [" + testint + "]");
            (campfire.Data as WorldData).Properties.Set("our_int", 77);
            testint = (campfire.Data as WorldData).Properties.Get<int>("our_int");
            Debug.Log("The Myint was updated to [" + testint + "]");

            // I will finish the tests...

            // test float
            // Add Int property
            FloatProperty MyFloat = new FloatProperty("our_float", 55.6f);
            (campfire.Data as WorldData).Properties.Add(MyFloat);

            // Test and test the update of the int property
            float testfloat = (campfire.Data as WorldData).Properties.Get<float>("our_float");
            Debug.Log("The Myfloat value is [" + testfloat + "]");

            // vector 2
            Vector2Property MyVec2 = new Vector2Property("our_vector2", new Vector2(11,22));
            (campfire.Data as WorldData).Properties.Add(MyVec2);

            // Test and test the update of the int property
            Vector2 testvec2 = (campfire.Data as WorldData).Properties.Get<Vector2>("our_vector2");
            Debug.Log("The Vector2 value is [" + testvec2 + "]");

            // vector 3
            Vector3Property MyVec3 = new Vector3Property("our_vector3", new Vector3(11, 22, 33));
            (campfire.Data as WorldData).Properties.Add(MyVec3);

            // Test and test the update of the int property
            Vector3 testvec3 = (campfire.Data as WorldData).Properties.Get<Vector3>("our_vector3");
            Debug.Log("The Vector3 value is [" + testvec3 + "]");

            // vector 4
            Vector4Property MyVec4 = new Vector4Property("our_vector4", new Vector4(11, 22, 33, 44));
            (campfire.Data as WorldData).Properties.Add(MyVec4);

            // Test and test the update of the int property
            Vector4 testvec4 = (campfire.Data as WorldData).Properties.Get<Vector4>("our_vector4");
            Debug.Log("The Vector4 value is [" + testvec4 + "]");

            // quaternion
            QuaternionProperty MyQuate = new QuaternionProperty("our_quat", new Quaternion(11, 22, 33, 44));
            (campfire.Data as WorldData).Properties.Add(MyQuate);

            // Test and test the update of the int property
            Quaternion testqua = (campfire.Data as WorldData).Properties.Get<Quaternion>("our_quat");
            Debug.Log("The Quaternion value is [" + testqua + "]");

            // color
            ColorProperty MyColor = new ColorProperty("our_color", new Vector4(1,2,3,1));
            (campfire.Data as WorldData).Properties.Add(MyColor);

            // Test and test the update of the int property
            Color testcol = (campfire.Data as WorldData).Properties.Get<Color>("our_color");
            Debug.Log("The Color value is [" + testcol + "]");

        }
    }
}