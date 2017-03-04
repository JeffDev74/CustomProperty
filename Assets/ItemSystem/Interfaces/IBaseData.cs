using UnityEngine;

namespace FPS
{
	public interface IBaseData
	{
        BaseData Data { get; set; }
        BaseNSData NSData { get; set; }
	}
}