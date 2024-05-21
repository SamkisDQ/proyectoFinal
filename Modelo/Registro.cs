using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;


namespace proyectoFinal.Modelo
{
    public class Registro
    {
        private const string url = "https://gourmetfoodservice.com/webServiceTimbrados/restRegistro.php";
        private readonly HttpClient cliente = new HttpClient();
        public ObservableCollection<Registro> registro;
        Empleados empleados = new Empleados();   

        public int id { get; set; }
        public int idEmpleado { get; set; }
        public Empleados empleado { get; set; }
        public DateTime fecha { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public int estado { get; set; }


        public async Task<ObservableCollection<Registro>> obtener()
        {
            var content = await cliente.GetStringAsync(url);
            List<Registro> listRegistro = JsonConvert.DeserializeObject<List<Registro>>(content);
            foreach (var registro in listRegistro)
            {
                registro.empleado = await empleados.ObtenerEmpleadoPorId(registro.idEmpleado);
            }
            registro = new ObservableCollection<Registro>(listRegistro);
            return registro;

        }

        public async Task<ObservableCollection<Registro>> obtenerPorCedula(string cedula)
        {
            try {
                string url2= "https://gourmetfoodservice.com/webServiceTimbrados/restRegistro.php?cedula=" + cedula;
                var content = await cliente.GetStringAsync(url2);
                List<Registro> listRegistro = JsonConvert.DeserializeObject<List<Registro>>(content);
                foreach (var registro in listRegistro)
                {
                    registro.empleado = await empleados.ObtenerEmpleadoPorId(registro.idEmpleado);
                }
                registro = new ObservableCollection<Registro>(listRegistro);
                return registro;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex.Message, "OK");
                return null;
            }

        }

        public async Task<ObservableCollection<Registro>> obtenerPorCedulaRangoFechas(string cedula,string fechaInicio, string fechaFin)
        {
            try
            {
                string url2 = $"https://gourmetfoodservice.com/webServiceTimbrados/restRegistro.php?cedula={cedula}&fechaInicio={fechaInicio}&fechaFin={fechaFin}";
                var content = await cliente.GetStringAsync(url2);
                List<Registro> listRegistro = JsonConvert.DeserializeObject<List<Registro>>(content);
                foreach (var registro in listRegistro)
                {
                    registro.empleado = await empleados.ObtenerEmpleadoPorId(registro.idEmpleado);
                }
                registro = new ObservableCollection<Registro>(listRegistro);
                return registro;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex.Message, "OK");
                return null;
            }

        }

        public bool insertarRegistro(Registro registroNew)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("idEmpleado", registroNew.idEmpleado.ToString());
                parametros.Add("latitud", registroNew.latitud);
                parametros.Add("longitud", registroNew.longitud);
                var result = cliente.UploadValues("https://gourmetfoodservice.com/webServiceTimbrados/restRegistro.php", "POST", parametros);
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
    }
}
