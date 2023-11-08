using PantAPIDreamsStyle.models.pant;

namespace PantAPIDreamsStyle.repositories
{
    public interface IPantRepository
    {
        Task<Pant> getPant(int idPant);
        Task<IEnumerable<Pant>> getListPants();
        Task<IEnumerable<Pant>> getListPantsUser(int idUser);
        Task<IEnumerable<Pocket>> getListPockets();
        Task<IEnumerable<Sticker>> getListStickers();
        Task<IEnumerable<Accesorie>> getListAccesories();
        Task<bool> addPant(Pant pant);
        Task<bool> removePant(int idPant);
        Task<bool> addPocketToPant(int idPant, int idPocket);
        Task<bool> removePocketToPant(int idPant, int idPocket);
        Task<bool> addStickerToPant(int idPant, Sticker sticker);
        Task<bool> removeStickerToPant(int idPant, int idSticker);
        Task<bool> addAccesorieToPant(int idPant, Accesorie accesorie);
        Task<bool> removeAccesorieToPant(int idPant, int idAccesorie);
        Task<bool> setFabricToPant(int idPant, int idFabric);
        Task<PantMeasurement> setMeasurementsToPant(int idPant, PantMeasurement pantMeasurement);
    }
}
