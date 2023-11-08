using PantAPIDreamsStyle.models.publication;

namespace PantAPIDreamsStyle.repositories
{
    public interface IPublicationRepository
    {
        Task<Publication> getPublication(int idPublication);
        Task<IEnumerable<Publication>> getListPublicationUser(int idPublication);
        Task<IEnumerable<Publication>> getListPublication();
        Task<bool> addPublication(Publication publication);
        Task<bool> removePublication(int idPublication);
        Task<bool> setStatusPublication(int idSale, char status);
    }
}
