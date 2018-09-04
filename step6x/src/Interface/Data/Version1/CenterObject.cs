
namespace Interface.Data.Version1
{
    public class CenterObject
    {
        public CenterObject(string type, int[] coordinates)
        {
            Type = type;
            Coordinates = coordinates;
        }

        public string Type { get; set; }
        public int[] Coordinates { get; set; }
    }
}
