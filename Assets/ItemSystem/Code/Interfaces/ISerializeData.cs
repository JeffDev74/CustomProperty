using UnityEngine;

namespace FPS
{
	public interface ISerializeData
	{
        string SerializeItemData();
        BaseItem FactoryCloneItemFromData();
    }
}