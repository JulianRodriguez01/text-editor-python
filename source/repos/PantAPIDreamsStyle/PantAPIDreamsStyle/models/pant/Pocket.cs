namespace PantAPIDreamsStyle.models.pant
{
    public class Pocket
    {
        public enum PocketType
        {
            typeOne,
            typeTwo,
            typeThree,
            typeFour,
            typeFive,
            typeSix,
        }
        public int id_pocket { get; set; }
        public PocketType pocket_type { get; set; }
        public double price { get; set; }
        public string position_pocket { get; set; }
        public string? description { get; set; }
    }
}
