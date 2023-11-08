using ApiPersons.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PantAPIDreamsStyle.models.publication;
using PantAPIDreamsStyle.repositories;

namespace ApiPersons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationRepository publicationRepository;

        public PublicationController(IPublicationRepository publicationRepository) {
            this.publicationRepository = publicationRepository;
        } 


        [HttpGet("users/publications/{idPublication}")]
        public async Task<IActionResult> GetPublication(int idPublication)
        {
            var pant = await publicationRepository.getPublication(idPublication);
            try
            {
                if (idPublication <= 0)
                {
                    return BadRequest("El ID del pantalon no es válido.");
                }
                if (pant == null)
                {
                    return NotFound($"No se encontro el pantalón: {idPublication}");
                }
                return Ok(pant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpGet("publications/")]
        public async Task<IActionResult> GetPublications()
        {
            var publications = await publicationRepository.getListPublication();
            try
            {
                return (publications == null || publications.Count() == 0) ? NotFound("No se encontraron publicaciones.") : Ok(publications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }
        
        [HttpGet("users/{idUser}/publications")]
        public async Task<IActionResult> GetPantsForUser(int idUser)
        {
            try
            {
                if (idUser <= 0)
                {
                    return BadRequest("El ID de usuario no es válido.");
                }
                var userPants = await publicationRepository.getListPublicationUser(idUser);
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

        [HttpPost("add-publication")]
        public async Task<IActionResult> addPublication([FromBody] Publication publication)
        {
            if (publication == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created("Created", await publicationRepository.addPublication(publication));
        }

        /*
        [HttpPut]
        public async Task<IActionResult> updateStatusPublication([FromBody] StatusRequest statusRequest)
        {
            if (statusRequest == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await publicationRepository.setStatusPublication(statusRequest.id_publication, statusRequest.status_update);
            return NoContent();
        }

        */
        [HttpDelete]
        public async Task<IActionResult> removePublication([FromBody] int idPublication)
        {
            await publicationRepository.removePublication(idPublication);
            return NoContent();
        }
    }
}
