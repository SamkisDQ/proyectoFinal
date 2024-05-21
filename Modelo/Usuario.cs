using Newtonsoft.Json;
using System.Net;

namespace proyectoFinal.Modelo
{
    public class Usuario
    {
        private const string url = "https://gourmetfoodservice.com/webServiceTimbrados/restRegistro.php";
        private readonly HttpClient cliente = new HttpClient();
        public int id { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public int estado { get; set; }


        public async Task<Usuario> validarUsuario(string usuario, string password)
        {
            try
            {
                var response = await cliente.GetStringAsync($"https://gourmetfoodservice.com/webServiceTimbrados/restUsuarios.php?usuario={usuario}&password={password}");
                return JsonConvert.DeserializeObject<Usuario>(response);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error al serializar/deserializar JSON: {ex.Message}");
                return null; // Retorna null en caso de error
            }
        }

        public bool insertarUsuario(Usuario usuarioNew)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("usuario", usuarioNew.usuario);
                parametros.Add("password", usuarioNew.password);
                var result = cliente.UploadValues("https://gourmetfoodservice.com/webServiceTimbrados/restUsuarios.php", "POST", parametros);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex.Message, "OK");
                return false;
            }

        }

        public async Task<bool> existeUsuario(string usuario)
        {
            try
            {
                var response = await cliente.GetStringAsync($"https://gourmetfoodservice.com/webServiceTimbrados/restUsuarios.php?usuario={usuario}");
                var usuarioObj = JsonConvert.DeserializeObject<Usuario>(response);

                if (usuarioObj != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error al serializar/deserializar JSON: {ex.Message}");
                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false; 
            }
        }
    }
}
