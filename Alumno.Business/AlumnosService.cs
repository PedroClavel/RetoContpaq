using Models.DTO;
using Models.Models;

namespace Alumno.Business
{
    public class AlumnosService
    {
        private readonly RegistroAlumnosContext _context;

        public AlumnosService(RegistroAlumnosContext context)
        {
            _context = context;
        }

        public async Task InsertAlumnoAsync(string nombres, string apellidoP, string apellidoM, int edad, int grado, string grupo, int? telefono, ResultadoDTO resultado)
        {
            var entidadInsert = new Models.Models.Alumno();

            ValidarCamposAlumno(nombres, apellidoP, apellidoM, edad, grado, grupo, telefono);

            entidadInsert.Nombres = nombres;
            entidadInsert.ApellidoPaterno = apellidoP;
            entidadInsert.ApellidoMaterno = apellidoM;
            entidadInsert.Edad = edad;
            entidadInsert.Grado = grado;
            entidadInsert.Grupo = grupo;
            entidadInsert.Telefono = telefono.HasValue ? telefono.Value : 0;
            entidadInsert.FechaAlta = DateTime.Now;

            _context.Add<Models.Models.Alumno>(entidadInsert);

            await _context.SaveChangesAsync();

            resultado.alumno = entidadInsert;
        }

        public async Task EditAlumnoAsync(int idAlumno, string nombres, string apellidoP, string apellidoM, int edad, int grado, 
            string grupo, int? telefono, ResultadoDTO resultado)
        {
            var entidadInsert = new Models.Models.Alumno();

            ValidarCamposAlumno(nombres, apellidoP, apellidoM, edad, grado, grupo, telefono);

            entidadInsert = GetAlumno(idAlumno);

            if (entidadInsert == null)
                throw new Exception($"No se encontró el alumno con el id {idAlumno} para su edición.");

            entidadInsert.Nombres = nombres;
            entidadInsert.ApellidoPaterno = apellidoP;
            entidadInsert.ApellidoMaterno = apellidoM;
            entidadInsert.Edad = edad;
            entidadInsert.Grado = grado;
            entidadInsert.Grupo = grupo;
            entidadInsert.Telefono = telefono.HasValue ? telefono.Value : entidadInsert.Telefono;
            entidadInsert.FechaModificacion = DateTime.Now;

            _context.Update<Models.Models.Alumno>(entidadInsert);

            await _context.SaveChangesAsync();

            resultado.alumno = entidadInsert;
        }

        public async Task DeleteAlumnoAsync(int idAlumno)
        {
            var entidadInsert = new Models.Models.Alumno();            

            entidadInsert = GetAlumno(idAlumno);

            if (entidadInsert == null)
                throw new Exception($"No se encontró el alumno con el id {idAlumno} para su eliminación.");           

            _context.Remove<Models.Models.Alumno>(entidadInsert);

            await _context.SaveChangesAsync();
        }

        public Models.Models.Alumno GetAlumno(int idAlumno)
        {
            return _context.Alumnos.FirstOrDefault(x => x.IdAlumno == idAlumno);            
        }

        public void ValidarCamposAlumno(string nombres, string apellidoP, string apellidoM, int edad, int grado, string grupo, int? telefono)
        {
            string mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(nombres))
            {
                mensaje += "El nombre no puede estar vacío.";
                mensaje += Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(apellidoP))
            {
                mensaje += "El apellido paterno no puede estar vacío.";
                mensaje += Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(apellidoM))
            {
                mensaje += "El apellido materno no puede estar vacío.";
                mensaje += Environment.NewLine;
            }

            if (edad == 0)
            {
                mensaje += "La edad no puede ser cero.";
                mensaje += Environment.NewLine;
            }

            if (edad > 110)
            {
                mensaje += "La edad no puede ser mayor a 110.";
                mensaje += Environment.NewLine;
            }

            if (grado == 0)
            {
                mensaje += "El grado no puede ser cero.";
                mensaje += Environment.NewLine;
            }

            if (grado == 6)
            {
                mensaje += "El grado no puede ser mayor a 6.";
                mensaje += Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(grupo))
            {
                mensaje += "El grupo no puede estar vacío.";
                mensaje += Environment.NewLine;
            }

            if (telefono.HasValue)
            {
                int digitos = telefono.Value.ToString().Length;

                if (digitos < 10 || digitos > 10)
                {
                    mensaje += "El telefono solo pueden ser 10 dígitos.";
                    mensaje += Environment.NewLine;
                }
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
                throw new Exception(mensaje);
        }

        public List<Models.Models.Alumno> GetListAlumnos()
        {

            return _context.Alumnos.ToList();
        }
    }
}