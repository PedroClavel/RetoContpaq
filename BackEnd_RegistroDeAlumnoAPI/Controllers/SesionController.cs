using Alumnos.Business;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Sesion.Business;
using Utilerias.Business;
using Utilerias.Business.Interfaces;

namespace BackEnd_RegistroDeAlumnoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Sesion")]
    [ApiController]
    public class SesionController : Controller
    {
        private readonly RegistroAlumnosContext _context;

        public SesionController(RegistroAlumnosContext context)
        {
            _context = context;
        }

        // GET: api/Sesion
        [HttpGet("IsUserValid/{usuario}/{password}")]
        public async Task<ActionResult<ResultadoDTO>> IsUserValid([FromRoute] string usuario, [FromRoute] string password)
        {
            ResultadoDTO resultado = new ResultadoDTO();

            try
            {
                var encriptarService = new EncriptarDesencriptarService();

                RecuperarDatosSesionService recuperarSersionService = new RecuperarDatosSesionService(_context, encriptarService);

                resultado.isCompleted = recuperarSersionService.IsUserValid(usuario, password);
            }
            catch (Exception error)
            {
                //Generar LOG
                RegistroErroresService registroErrores = new RegistroErroresService();
                registroErrores.AddLog(error.Message);

                resultado.isCompleted = false;
                resultado.messageError = error.Message;
            }

            return Resultado(resultado);
        }

        private static ResultadoDTO Resultado(ResultadoDTO resultado) =>
     new ResultadoDTO
     {
         isCompleted = resultado.isCompleted,
         messageError = resultado.messageError,
         alumno = resultado.alumno
     };
    }
}