using Alumno.Business;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Utilerias.Business;

namespace BackEnd_RegistroDeAlumnoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Alumno")]
    [ApiController]
    public class AlumnoController : Controller
    {
        private readonly RegistroAlumnosContext _context;

        public AlumnoController(RegistroAlumnosContext context)
        {
            _context = context;
        }

        [HttpPost("InsertAlumno/{nombres}/{apellidoP}/{apellidoM}/{edad}/{grado}/{grupo}/{telefono?}")]
        public async Task<ActionResult<ResultadoDTO>> InsertAlumno([FromRoute] string nombres, [FromRoute] string apellidoP, [FromRoute] string apellidoM
            ,[FromRoute] int edad, [FromRoute] int grado, [FromRoute] string grupo, [FromRoute] int? telefono)
        {
            ResultadoDTO resultado = new ResultadoDTO();

            try
            {
                AlumnosService alumnosService = new AlumnosService(_context);

                await alumnosService.InsertAlumnoAsync(nombres, apellidoP, apellidoM, edad, grado, grupo, telefono, resultado);

                resultado.isCompleted = true;
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

        [HttpPost("ValidateAlumno/{nombres}/{apellidoP}/{apellidoM}/{edad}/{grado}/{grupo}/{telefono?}")]
        public string ValidateAlumno([FromRoute] string nombres, [FromRoute] string apellidoP, [FromRoute] string apellidoM
            , [FromRoute] int edad, [FromRoute] int grado, [FromRoute] string grupo, [FromRoute] int? telefono)
        {
            string resultado = string.Empty;

            try
            {
                AlumnosService alumnosService = new AlumnosService(_context);

                alumnosService.ValidarCamposAlumno(nombres, apellidoP, apellidoM, edad, grado, grupo, telefono);
            }
            catch (Exception error)
            {
                resultado = error.Message.Replace("\r\n", " ");
            }

            return resultado;
        }

        [HttpPost("EditAlumno/{idAlumno}/{nombres}/{apellidoP}/{apellidoM}/{edad}/{grado}/{grupo}/{telefono?}")]
        public async Task<ActionResult<ResultadoDTO>> EditAlumno([FromRoute] int idAlumno, [FromRoute] string nombres, [FromRoute] string apellidoP, [FromRoute] string apellidoM
            , [FromRoute] int edad, [FromRoute] int grado, [FromRoute] string grupo, [FromRoute] int? telefono)
        {
            ResultadoDTO resultado = new ResultadoDTO();

            try
            {
                AlumnosService alumnosService = new AlumnosService(_context);

                await alumnosService.EditAlumnoAsync(idAlumno, nombres, apellidoP, apellidoM, edad, grado, grupo, telefono, resultado);

                resultado.isCompleted = true;

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

        [HttpPost("DeleteAlumno/{idAlumno}")]
        public async Task<ActionResult<ResultadoDTO>> DeleteAlumno([FromRoute] int idAlumno)
        {
            ResultadoDTO resultado = new ResultadoDTO();

            try
            {
                AlumnosService alumnosService = new AlumnosService(_context);

                await alumnosService.DeleteAlumnoAsync(idAlumno);

                resultado.isCompleted = true;
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

        [HttpGet("GetListAlumnos")]
        public List<Models.Models.Alumno> GetListAlumnos()
        {
            var listaAlumnos = new List<Models.Models.Alumno>();

            try
            {
                AlumnosService alumnosService = new AlumnosService(_context);

                listaAlumnos = alumnosService.GetListAlumnos();
            }
            catch (Exception error)
            {
                //Generar LOG
                RegistroErroresService registroErrores = new RegistroErroresService();
                registroErrores.AddLog(error.Message);
            }

            return listaAlumnos;
        }

        [HttpGet("GetAlumno/{idAlumno}")]
        public ResultadoDTO GetAlumno([FromRoute] int idAlumno)
        {
            ResultadoDTO resultado = new ResultadoDTO();

            try
            {
                AlumnosService alumnosService = new AlumnosService(_context);

                resultado.alumno = alumnosService.GetAlumno(idAlumno);

                resultado.isCompleted = true;
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