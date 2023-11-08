using PantAPIDreamsStyle.models.pant;
using PantAPIDreamsStyle.models.sale;

namespace PantAPIDreamsStyle.repositories
{
    public interface ISaleRepository
    {
        Task<Sale> getSale(int idSale);
        Task<IEnumerable<Sale>> getListSalesUser(int idUser);
        Task<IEnumerable<Sale>> getListSales();
        Task<bool> addSale(Sale sale);
        Task<bool> addSaleDetails(SaleDetails saleDetails);
        Task<bool> setStatusSale(int idSale, string status);
        Task<bool> removeSale(int idSale);
    }
}
