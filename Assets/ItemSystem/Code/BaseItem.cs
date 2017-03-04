
namespace FPS
{
    [System.Serializable]
    public abstract class BaseItem : IBaseData
    {
        public abstract BaseData Data { get; set; }

        public abstract BaseNSData NSData { get; set; }
    }
}