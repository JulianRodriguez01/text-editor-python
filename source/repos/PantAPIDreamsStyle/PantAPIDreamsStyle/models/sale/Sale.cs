namespace PantAPIDreamsStyle.models.sale
{
    public class Sale
    {
        public int id_sale { get; set; }
        public int id_user { get; set; }
        public DateTime sale_date { get; set; }
        public string status { get; set; }
    }
}