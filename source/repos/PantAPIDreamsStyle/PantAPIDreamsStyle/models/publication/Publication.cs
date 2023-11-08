namespace PantAPIDreamsStyle.models.publication
{
    public class Publication
    {
        public int id_publication { get; set; }
        public int id_user { get; set; }
        public int id_pant { get; set; }
        public DateTime publication_date { get; set; }
        public char status_publication { get; set; }
    }
}
