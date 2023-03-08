using Models.Models;
using Utilerias.Business.Interfaces;

namespace Sesion.Business
{
    public class RecuperarDatosSesionService
    {
        private readonly RegistroAlumnosContext _context;
        private readonly IEncriptarDesencriptarService _encriptarService;

        public RecuperarDatosSesionService(RegistroAlumnosContext context, IEncriptarDesencriptarService encriptarService)
        {
            _context = context;
            _encriptarService = encriptarService;
        }

        public bool IsUserValid(string usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("No se recibio el parámetro usuario");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("No se recibio el parámetro password");

            string passwordEncode = _encriptarService.Base64Encode(password);

            var entidad = _context.Sesions.FirstOrDefault(x => x.Usuario == usuario && x.Pass == passwordEncode);

            if (entidad == null)
                throw new Exception("El usuario o la contraseña son incorrectas.");

            return true;
        }
    }
}