namespace PantAPIDreamsStyle.models.pant
{
    public class Accesorie
    {
        public enum AccesorieType
        {
            long_chain,
            small_chain,
        }
        public int id_accessorie { get; set; }
        public AccesorieType accesorie_type { get; set; }
        public double price { get; set; }
        public string? adittional_data { get; set; }
    }
}
