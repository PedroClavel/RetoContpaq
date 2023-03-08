namespace Utilerias.Business
{
    public class RegistroErroresService
    {
    private string path = Path.GetDirectoryName(Directory.GetCurrentDirectory()) + "\\LOGS\\";//AppDomain.CurrentDomain.BaseDirectory + "LOGS\\";

        public void AddLog(string log)
        {
            CreateDirectory();

            string nombre = GetNameFile();
            string cadena = string.Empty;

            cadena += " --> Error del día: " + DateTime.Now + Environment.NewLine;
            cadena += log + Environment.NewLine;
            cadena += Environment.NewLine;

            StreamWriter sw = new StreamWriter(path + "/" + nombre, true);

            sw.Write(cadena);
            sw.Close();
        }

        private string GetNameFile()
        {
            string nombre = string.Empty;

            nombre = "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt";

            return nombre;
        }

        private void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}