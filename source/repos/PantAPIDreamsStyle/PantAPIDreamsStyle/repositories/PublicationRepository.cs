using MySqlConnector;
using Dapper;
using System.Security.Policy;
using System.Xml.Linq;
using PantAPIDreamsStyle.models.publication;
using PantAPIDreamsStyle.models.sale;
using PantAPIDreamsStyle.models.user;
using Org.BouncyCastle.Crypto;

namespace PantAPIDreamsStyle.repositories
{
    public class PublicationRepository : IPublicationRepository
    {
        public const string SQL_GET_PUBLICATIONS = "SELECT * FROM dreams_style.publication";
        public const string SQL_GET_PUBLICATION = "SELECT * FROM dreams_style.publication WHERE id_publication = @id_publication";
        public const string SQL_GET_PUBLICATION_USER = "CALL get_publication_user(@id_user)";
        public const string SQL_ADD_PUBLICATION = "INSERT INTO `dreams_style`.`publication` (`id_user`, `id_pant`, `publication_date`, `status_publication`) VALUES (@id_user, @id_pant, now(), @status_publication)";
        public const string SQL_SET_STATUS = "UPDATE `dreams_style`.`publication` SET `status_publication` = @status WHERE (`id_publication` = @id_publication)";
        public const string SQL_ADD_REMOVE_PUBLICATION = "DELETE FROM `dreams_style`.`publication` WHERE(`id_publication` = @id_publication)";

        private readonly MySqlConfiguration _connectionConfiguration;

        public PublicationRepository(MySqlConfiguration connectionConfiguration)
        {
            _connectionConfiguration = connectionConfiguration;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionConfiguration.connection);
        }

        public async Task<bool> addPublication(Publication publication)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_ADD_PUBLICATION, new { id_user = publication.id_user, id_pant = publication.id_pant,  status_publication = publication.status_publication });
            return result > 0;
        }

        public async Task<IEnumerable<Publication>> getListPublication()
        {
            var db = dbConnection();
            return await db.QueryAsync<Publication>(SQL_GET_PUBLICATIONS, new { });
        }

        public async Task<IEnumerable<Publication>> getListPublicationUser(int idUser)
        {
            var db = dbConnection();
            return await db.QueryAsync<Publication>(SQL_GET_PUBLICATION_USER, new { id_user = idUser });
        }

        public async Task<Publication> getPublication(int idPublication)
        {
            var db = dbConnection();
            return await db.QueryFirstAsync<Publication>(SQL_GET_PUBLICATION, new { id_publication = idPublication });
        }

        public async Task<bool> removePublication(int idPublication)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_ADD_REMOVE_PUBLICATION, new { id_publication = idPublication });
            return result > 0;
        }

        public async Task<bool> setStatusPublication(int idSale, char status)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_SET_STATUS, new { id_sale = idSale, status = status });
            return result > 0;
        }


        /*
        

        public async Task<IEnumerable<Publication>> getListSales()
        {
            var db = dbConnection();
            return await db.QueryAsync<Publication>(SQL_GET_SALES, new { });
        }

        public async Task<Publication> getSale(int idSale)
        {
            var db = dbConnection();
            return await db.QueryFirstAsync<Sale>(SQL_GET_SALE, new { id_sale = idSale});
        }

        public async Task<IEnumerable<Publication>> getListSalesUser(int idUser)
        {
            var db = dbConnection();
            return await db.QueryAsync<Sale>(SQL_GET_SALES_USER, new { id_user = idUser});
        }

        public async Task<bool> addSale(Publication sale)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_ADD_PUBLICATION, new { id_user = sale.id_user, sale_date = sale.sale_date, status = sale.status });
            return result > 0;
        }

        public async Task<bool> addSaleDetails(SaleDetails saleDetails)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_ADD_SALE_DETAILS, new { id_pant = saleDetails.id_pant, quantity = saleDetails.quantity, price_total = saleDetails.price_total });
            return result > 0;
        }

        public async Task<bool> setStatusSale(int idSale, string status)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> removeSale(int idSale)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(@SQL_ADD_REMOVE_SALE, new { idPant = idSale });
            return result > 0;
        }*/
    }
}
