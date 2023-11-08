using MySqlConnector;
using Dapper;
using System.Security.Policy;
using System.Xml.Linq;
using PantAPIDreamsStyle.models.sale;
using PantAPIDreamsStyle.models.user;

namespace PantAPIDreamsStyle.repositories
{
    public class SaleRepository : ISaleRepository
    {
        public const string SQL_GET_SALES = "SELECT * FROM dreams_style.sale";
        public const string SQL_GET_SALE = "SELECT * FROM dreams_style.sale WHERE id_sale = @id_sale";
        public const string SQL_GET_SALES_USER = "";
        public const string SQL_ADD_SALE = "INSERT INTO `dreams_style`.`sale` (`id_user`, `sale_date`, `status`) VALUES(@id_user, @sale_date, @status)";
        public const string SQL_ADD_SALE_DETAILS = "INSERT INTO `dreams_style`.`sale_details` (`id_pant`, `quantity`, `price_total`) VALUES (@id_pant, @quantity, @price_total);";
        public const string SQL_ADD_REMOVE_SALE = "DELETE FROM `dreams_style`.`sale` WHERE(`id_sale` = @id_sale)";

        private readonly MySqlConfiguration _connectionConfiguration;

        public SaleRepository(MySqlConfiguration connectionConfiguration)
        {
            _connectionConfiguration = connectionConfiguration;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionConfiguration.connection);
        }

        public async Task<IEnumerable<Sale>> getListSales()
        {
            var db = dbConnection();
            return await db.QueryAsync<Sale>(SQL_GET_SALES, new { });
        }

        public async Task<Sale> getSale(int idSale)
        {
            var db = dbConnection();
            return await db.QueryFirstAsync<Sale>(SQL_GET_SALE, new { id_sale = idSale});
        }

        public async Task<IEnumerable<Sale>> getListSalesUser(int idUser)
        {
            var db = dbConnection();
            return await db.QueryAsync<Sale>(SQL_GET_SALES_USER, new { id_user = idUser});
        }

        public async Task<bool> addSale(Sale sale)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_ADD_SALE, new { id_user = sale.id_user, sale_date = sale.sale_date, status = sale.status });
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
            var result = await db.ExecuteAsync(@SQL_ADD_REMOVE_SALE, new { id_sale = idSale });
            return result > 0;
        }

        /*
        public async Task<IEnumerable<Pant>> getListPants()
        {
            var db = dbConnection();
            return await db.QueryAsync<Pant>(SQL_GET_PANTS, new { });
        }

        public async Task<IEnumerable<Pant>> getListPantsUser(int idUser)
        {
            var db = dbConnection();
            return await db.QueryAsync<Pant>(SQL_GET_PANTS_USER, new { });
        }

        public async Task<IEnumerable<Pocket>> getListPockets()
        {
            var db = dbConnection();
            return await db.QueryAsync<Pocket>(SQL_GET_POCKETS, new { });
        }

        public async Task<IEnumerable<Sale>> getListStickers()
        {
            var db = dbConnection();
            return await db.QueryAsync<Sale>(SQL_GET_STICKERS, new { });
        }

        public async Task<IEnumerable<Accesorie>> getListAccesories()
        {
            var db = dbConnection();
            return await db.QueryAsync<Accesorie>(SQL_GET_ACCESORIES, new { });
        }

        public async Task<bool> addPant(Pant pant)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_ADD_PANT, new { id_pant_measurement = pant.id_pant_measurement, id_fabric = pant.id_fabric, type_of_closure = pant.type_of_closure});
            return result > 0;
        }

        public async Task<bool> removePant(int idPant)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(@"DELETE FROM `dreams_style`.`custom_pant` WHERE(`id_pant` = @idPant", new { idPant = idPant });
            return result > 0;
        }

        public async Task<bool> addAccesorieToPant(int idPant, Accesorie accesorie)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(@"DELETE FROM `dreams_style`.`custom_pant` WHERE(`id_pant` = @idPant", new { idPant = idPant });
            return result > 0;
        }

        public async Task<bool> removeAccesorieToPant(int idPant, int idAccesorie)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(@"DELETE FROM `dreams_style`.`custom_pant` WHERE(`id_pant` = @idPant", new { idPant = idPant });
            return result > 0;
        }

        public async Task<bool> addPocketToPant(int idPant, int idPocket)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_ADD_POCKET_PANT, new { id_pant = idPant, id_pocket = idPocket });
            return result > 0;
        }

        public async Task<bool> removePocketToPant(int idPant, int idPocket)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(@SQL_REMOVE_POCKET_PANT, new { id_pant = idPant, id_pocket = idPocket });
            return result > 0;
        }

        public async Task<bool> addStickerToPant(int idPant, Sale sticker)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_ADD_STICKER_PANT, new { id_pant = idPant, id_sticker = sticker.id_sticker });
            return result > 0;
        }

        public async Task<bool> removeStickerToPant(int idPant, int idSticker)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(SQL_REMOVE_STICKER_PANT, new { id_pant = idPant, id_sticker = idSticker });
            return result > 0;
        }

        public async Task<bool> setFabricToPant(int idPant, int idFabric)
        {
            var db = dbConnection();
            var result = await db.ExecuteAsync(@SQL_SET_FABRIC, new { id_pant = idPant, id_fabric = idFabric });
            return result > 0;
        }

        public async Task<PantMeasurement> setMeasurementsToPant(int idPant, PantMeasurement pantMeasurement)
        {
            var db = dbConnection();
            var result = await db.QueryFirstAsync<PantMeasurement>(SQL_SET_MEASUREMENT, new {
                pantMeasurement.contour_size_cm, pantMeasurement.crotch_length_cm,
                pantMeasurement.wide_hip_cm, pantMeasurement.wide_boot_cm,
                pantMeasurement.wide_thigh_cm, pantMeasurement.long_cm });
            if (result != null) {
                return result;
            }
            return await db.QueryFirstAsync<PantMeasurement>(SQL_ADD_MEASUREMENT, new 
            {
                idPant = idPant,
                pantMeasurement.contour_size_cm,
                pantMeasurement.crotch_length_cm,
                pantMeasurement.wide_hip_cm,
                pantMeasurement.wide_boot_cm,
                pantMeasurement.wide_thigh_cm,
                pantMeasurement.long_cm
            }); 
        }*/
    }
}
