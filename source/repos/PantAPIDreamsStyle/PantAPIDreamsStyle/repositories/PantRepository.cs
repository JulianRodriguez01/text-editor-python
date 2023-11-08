using MySqlConnector;
using Dapper;
using System.Security.Policy;
using System.Xml.Linq;
using PantAPIDreamsStyle.models.pant;

namespace PantAPIDreamsStyle.repositories
{
    public class PantRepository : IPantRepository
    {
        public const string SQL_GET_PANTS = "CALL get_pants()";
        public const string SQL_GET_PANT = "CALL get_pant(@id_pant)";
        public const string SQL_GET_POCKETS = "SELECT * FROM dreams_style.pocket";
        public const string SQL_GET_STICKERS = "SELECT * FROM dreams_style.sticker";
        public const string SQL_GET_ACCESORIES = "SELECT * FROM dreams_style.accessorie";
        public const string SQL_GET_PANTS_USER = "CALL get_pant_user(@document_number)";
        public const string SQL_ADD_PANT = "INSERT INTO `dreams_style`.`custom_pant` (`id_pant_measurement`, `id_fabric`, `type_of_closure`) VALUES (@id_pant_measurement, @id_fabric, @type_of_closure)";
        public const string SQL_SET_MEASUREMENT = "CALL set_measurement(@contour_size_cm, @crotch_length_cm, @wide_hip_cm, @wide_boot_cm, @wide_thigh_cm, @long_cm)";
        public const string SQL_ADD_MEASUREMENT = "CALL add_measurement(@contour_size_cm, @crotch_length_cm, @wide_hip_cm, @wide_boot_cm, @wide_thigh_cm, @long_cm)";
        public const string SQL_SET_FABRIC = "UPDATE `dreams_style`.`custom_pant` SET `id_fabric` = @id_fabric WHERE (`id_pant` = @id_pant);";
        public const string SQL_ADD_POCKET_PANT = "INSERT INTO `dreams_style`.`pant_pocket` (`id_pant`, `id_pocket`) VALUES(@id_pant, @id_pocket)";
        public const string SQL_REMOVE_POCKET_PANT = "DELETE FROM `dreams_style`.`pant_pocket` WHERE `id_pant` = @id_pant AND `id_pocket` = @id_pocket";
        public const string SQL_ADD_STICKER_PANT = "INSERT INTO `dreams_style`.`pant_sticker` (`id_pant`, `id_sticker`) VALUES(@id_pant, @id_sticker)";
        public const string SQL_REMOVE_STICKER_PANT = "DELETE FROM `dreams_style`.`pant_sticker` WHERE `id_pant` = @id_pant AND `id_sticker` = @id_sticker";

        private readonly MySqlConfiguration _connectionConfiguration;

        public PantRepository(MySqlConfiguration connectionConfiguration)
        {
            _connectionConfiguration = connectionConfiguration;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionConfiguration.connection);
        }

        public async Task<Pant> getPant(int idPant)
        {
            var db = dbConnection();
            var result = await db.QueryFirstOrDefaultAsync<Pant>(SQL_GET_PANT, new { id_pant = idPant });
            return result;
        }

        public async Task<IEnumerable<Pant>> getListPants()
        {
            var db = dbConnection();
            return await db.QueryAsync<Pant>(SQL_GET_PANTS, new { });
        }

        public async Task<IEnumerable<Pant>> getListPantsUser(int idUser)
        {
            var db = dbConnection();
            return await db.QueryAsync<Pant>(SQL_GET_PANTS_USER, new { document_number = idUser });
        }

        public async Task<IEnumerable<Pocket>> getListPockets()
        {
            var db = dbConnection();
            return await db.QueryAsync<Pocket>(SQL_GET_POCKETS, new { });
        }

        public async Task<IEnumerable<Sticker>> getListStickers()
        {
            var db = dbConnection();
            return await db.QueryAsync<Sticker>(SQL_GET_STICKERS, new { });
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

        public async Task<bool> addStickerToPant(int idPant, Sticker sticker)
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
        }
    }
}
