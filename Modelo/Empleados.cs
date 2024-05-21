using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace proyectoFinal.Modelo
{
    public class Empleados
    {
        private readonly HttpClient cliente = new HttpClient();
        public ObservableCollection<Empleados> empleadosList;

        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public int edad { get; set; }
        public int estado { get; set; }


        public async Task<Empleados> ObtenerEmpleadoPorId(int idEmpleado)
        {
            try
            {
                var response = await cliente.GetStringAsync($"https://gourmetfoodservice.com/webServiceTimbrados/restEmpleados.php?id={idEmpleado}");
                return JsonConvert.DeserializeObject<Empleados>(response);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error al serializar/deserializar JSON: {ex.Message}");
                return null; // Retorna null en caso de error
            }
        }

        public async Task<Empleados> ObtenerEmpleadoPorCedula(string cedula)
        {
            try
            {
                var response = await cliente.GetStringAsync($"https://gourmetfoodservice.com/webServiceTimbrados/restEmpleados.php?cedula={cedula}");
                return JsonConvert.DeserializeObject<Empleados>(response);
            }
            catch (JsonSerializationException ex)
            {
                // Manejar la excepción aquí
                Console.WriteLine($"Error al serializar/deserializar JSON: {ex.Message}");
                return null; // Retorna null en caso de error
            }
        }

        public async Task<ObservableCollection<Empleados>> obtenerPorCedulaAll(string cedulaBuscar)
        {
            string url = $"https://gourmetfoodservice.com/webServiceTimbrados/restEmpleados.php?cedula={cedulaBuscar}&opcion=1";
            var content = await cliente.GetStringAsync(url);
            List<Empleados> listEmpleados = JsonConvert.DeserializeObject<List<Empleados>>(content);
            empleadosList = new ObservableCollection<Empleados>(listEmpleados);
            return empleadosList;
        }

        public async Task<ObservableCollection<Empleados>> obtenerAll()
        {
            string url = $"https://gourmetfoodservice.com/webServiceTimbrados/restEmpleados.php";
            var content = await cliente.GetStringAsync(url);
            List<Empleados> listEmpleados = JsonConvert.DeserializeObject<List<Empleados>>(content);
            empleadosList = new ObservableCollection<Empleados>(listEmpleados);
            return empleadosList;
        }

        public bool insertarEmpleado(Empleados EmpleadoNew)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("nombre", EmpleadoNew.nombre);
                parametros.Add("apellido", EmpleadoNew.apellido);
                parametros.Add("cedula", EmpleadoNew.cedula);
                parametros.Add("edad", EmpleadoNew.edad.ToString());
                var result = cliente.UploadValues("https://gourmetfoodservice.com/webServiceTimbrados/restEmpleados.php", "POST", parametros);
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

        public async Task<bool> actualizarEmpleado(Empleados empleadoNew)
        {
            try
            {
                string url = $"https://gourmetfoodservice.com/webServiceTimbrados/restEmpleados.php?id={empleadoNew.id}&nombre={empleadoNew.nombre}&apellido={empleadoNew.apellido}&cedula={empleadoNew.cedula}&edad={empleadoNew.edad}";

                HttpResponseMessage response = await cliente.PutAsync(url, null);

                if (response.IsSuccessStatusCode)
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
