namespace PantAPIDreamsStyle.models.pant
{
    public class Pant
    {
        public int id_pant { get; set; }
        public int id_fabric { get; set; }
        public int id_pant_measurement { get; set; }
        public List<Accesorie> accesorieList { get; set; }
        public List<Pocket> pocketList { get; set; }
        public List<Sticker> stickerList { get; set; }
        public string type_of_closure { get; set; }
    }
}
