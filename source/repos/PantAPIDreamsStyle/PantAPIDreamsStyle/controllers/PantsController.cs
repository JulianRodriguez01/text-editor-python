using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PantAPIDreamsStyle.models.pant;
using PantAPIDreamsStyle.repositories;

namespace ApiPersons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PantsController : ControllerBase
    {
        private readonly IPantRepository pantRepository;

        public PantsController(IPantRepository pantRepository) {
            this.pantRepository = pantRepository;
        } 

        [HttpGet("users/pants/{idPant}")]
        public async Task<IActionResult> GetPant(int idPant)
        {
            var pant = await pantRepository.getPant(idPant);
            try
            {
                if (idPant <= 0)
                {
                    return BadRequest("El ID del pantalon no es válido.");
                }
                if (pant == null)
                {
                    return NotFound($"No se encontro el pantalón: {idPant}");
                }
                return Ok(pant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpGet("pants/")]
        public async Task<IActionResult> GetPants()
        {
            var pants = await pantRepository.getListPants();
            try
            {
                return (pants == null || pants.Count() == 0) ? NotFound("No se encontraron pantalones.") : Ok(pants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }
        
        [HttpGet("users/{idUser}/pants")]
        public async Task<IActionResult> GetPantsForUser(int idUser)
        {
            try
            {
                if (idUser <= 0)
                {
                    return BadRequest("El ID de usuario no es válido.");
                }
                var userPants = await pantRepository.getListPantsUser(idUser);
                if (userPants == null)
                {
                    return NotFound($"No se encontraron pantalones para el usuario con ID {idUser}.");
                }
                return Ok(userPants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpGet("pockets/")]
        public async Task<IActionResult> GetPockets()
        {
            var pants = await pantRepository.getListPockets();
            try
            {
                return (pants == null || pants.Count() == 0) ? NotFound("No se encontraron pantalones.") : Ok(pants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpGet("stickers/")]
        public async Task<IActionResult> GetStickers()
        {
            var pants = await pantRepository.getListStickers();
            try
            {
                return (pants == null || pants.Count() == 0) ? NotFound("No se encontraron pantalones.") : Ok(pants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpGet("accesories/")]
        public async Task<IActionResult> GetAccesories()
        {
            var pants = await pantRepository.getListAccesories();
            try
            {
                return (pants == null || pants.Count() == 0) ? NotFound("No se encontraron pantalones.") : Ok(pants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpPut("users/pants/fabric/")]
        public async Task<IActionResult> SetFabricPant([FromBody] FabricRequest fabricRequest)
        {
            if (fabricRequest == null)
            {
                return BadRequest("Esta vacía la consulta, por favor llenar los campos :).");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Error en la consulta");
            }
            await pantRepository.setFabricToPant(fabricRequest.UserId, fabricRequest.PantId);
            return Ok("Modifico correctamente la tela del pantalón");
        }
        
        [HttpPut("users/pants/measurement/")]
        public async Task<IActionResult> SetMeasurementPant([FromBody] MeasurementRequest measurementRequest)
        {
            if (measurementRequest == null)
            {
                return BadRequest("Esta vacía la consulta, por favor llenar los campos :).");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Error en la consulta.");
            }
            await pantRepository.setMeasurementsToPant(measurementRequest.id_pant, measurementRequest.pantMeasurement);
            return Ok("Modifico correctamente la talla del pantalón.");
        }

        [HttpPost("add-pant")]
        public async Task<IActionResult> AddPant([FromBody] Pant pant)
        {
            if (pant == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El cuerpo de la solicitud no puede estar vacío."
                });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationProblemDetails(ModelState)
                {
                    Title = "La solicitud contiene errores de validación."
                });
            }
            var addedPant = await pantRepository.addPant(pant);
            return CreatedAtAction("GetPant", addedPant);
        }

        [HttpPost("add-pocket-pant")]
        public async Task<IActionResult> AddPocketToPant([FromBody] PantRequest pantRequest)
        {
            if (pantRequest == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El cuerpo de la solicitud no puede estar vacío."
                });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationProblemDetails(ModelState)
                {
                    Title = "La solicitud contiene errores de validación."
                });
            }
            var addedPant = await pantRepository.addPocketToPant(pantRequest.id_pant, pantRequest.id_set_value);
            return CreatedAtAction("GetPant", addedPant);
        }

        [HttpPost("add-sticker-pant")]
        public async Task<IActionResult> AddStickerToPant([FromBody] StatusRequest stickerRequest)
        {
            if (stickerRequest == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El cuerpo de la solicitud no puede estar vacío."
                });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationProblemDetails(ModelState)
                {
                    Title = "La solicitud contiene errores de validación."
                });
            }
            var addedPant = await pantRepository.addStickerToPant(stickerRequest.id_pant, stickerRequest.sticker);
            return CreatedAtAction("GetPant", addedPant);
        }

        [HttpDelete("remove-pant/{pant_id}")]
        public async Task<IActionResult> RemovePant(int pant_id)
        {
            if (pant_id <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El identificador del pantalón no es válido."
                });
            }
            var deletedPant = await pantRepository.removePant(pant_id);
            if (deletedPant == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Recurso no encontrado",
                    Detail = "El pantalón con el ID especificado no existe."
                });
            }
            return NoContent();
        }

        [HttpDelete("remove-pocket-pant/{pant_id}/{pocket_id}")]
        public async Task<IActionResult> RemovePocketToPant([FromBody] PantRequest pantRequest)
        {
            if (pantRequest.id_pant <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El identificador del pantalón no es válido."
                });
            }
            var deletedPant = await pantRepository.removePocketToPant(pantRequest.id_pant, pantRequest.id_set_value);
            if (deletedPant == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Recurso no encontrado",
                    Detail = "El pantalón con el ID especificado no existe."
                });
            }
            return NoContent();
        }

        [HttpDelete("remove-sticker-pant/{pant_id}/{sticker_id}")]
        public async Task<IActionResult> RemoveStickerToPant([FromBody] PantRequest pantRequest)
        {
            if (pantRequest.id_pant <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El identificador del pantalón no es válido."
                });
            }
            var deletedPant = await pantRepository.removeStickerToPant(pantRequest.id_pant, pantRequest.id_set_value);
            if (deletedPant == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Recurso no encontrado",
                    Detail = "El pantalón con el ID especificado no existe."
                });
            }
            return NoContent();
        }
        /*
        [HttpPost]
        public async Task<IActionResult> addUser([FromBody] Pocket user)
        {
            if (user == null) 
                return BadRequest();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created("Created", await userRepository.addUser(user));
        }

        [HttpPut]
        public async Task<IActionResult> updateUser([FromBody] Pocket user)
        {
            if (user == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await userRepository.updateUser(user);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> deleteUser([FromBody] string document_number)
        {
            await userRepository.removeUser(new Pocket { document_number = document_number });
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Pocket isAuthenticated = await userRepository.login(loginModel.Email, loginModel.Password);
            if (isAuthenticated == null)
            {
                return Unauthorized();
            }
            return Ok(new { message = "Ingresaste con exito a DreamsStyle." });
        }


        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendRecoveryEmail([FromBody] RecoveryEmailModel recoveryEmailModel)
        {
            try
            {
                var user = await userRepository.getUserRecoveryAccount(recoveryEmailModel.Email);
                if(user == null)
                {
                    return BadRequest(new { message = "Error al enviar el correo de recuperación." });
                }
                var mailHelper = new MailHelper();
                string userName = user.name_user;
                string token = TokenGenerator.generateRandomToken();
                await userRepository.setToken(recoveryEmailModel.Email, token);
                mailHelper.SendEmail(recoveryEmailModel.Email, userName, token );
                return Ok(new { message = "Correo de recuperación enviado con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al enviar el correo de recuperación." });
            }
        }

        [HttpPost("updatePassword")]
        public async Task<IActionResult> SetNewPassword([FromBody] UpdatePasswordModel updatePasswordModel)
        {
            try
            {
                await userRepository.UpdateNewPassword(updatePasswordModel.Email, updatePasswordModel.Token, updatePasswordModel.NewPassword);
                return Ok(new { message = "Contraseña actualizada con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al actualizar la contraseña." });
            }
        }
        */

        /*
        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> recoverPassword([FromBody] string email)
        {
            User user = await userRepository.getUser(email);
            if (user == null)
            {
                return BadRequest();
            }
            string myToken = await TokenGenerator.generatePasswordResetTokenAsync();
            string link = Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
            _mailHelper.SendMail(request.Email, "Password Recover", $"<h1>Password Recover</h1>" +
                $"Click on the following link to change your password:<p>" +
                $"<a href = \"{link}\">Change Password</a></p>");

            return Ok(new Response { IsSuccess = true });
        }
        */
    }
}
