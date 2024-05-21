using Newtonsoft.Json.Linq;
using proyectoFinal.Modelo;
using System;
using System.Globalization;

namespace proyectoFinal.Vistas;

public partial class VerRegistro : ContentPage
{
	Empleados emp = new Empleados();
	Registro registro = new Registro();
	public VerRegistro(Empleados empleados)
	{
		InitializeComponent();
		emp= empleados;
		lbl_empleado.Text= "Empleado: "+emp.nombre+" "+ emp.apellido+ " - "+emp.cedula;
	}

    private async void btn_consultar_Clicked(object sender, EventArgs e)
    {
        string fechaInicio = dp_fechaInicio.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        string fechaFin = dp_fechaFin.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        listaRegistro.ItemsSource = await registro.obtenerPorCedulaRangoFechas(emp.cedula, fechaInicio, fechaFin);
		//await DisplayAlert("wesa", emp.cedula + fechaInicio + fechaFin, "ok");

    }

    private async void btn_verUbicacion_Clicked(object sender, EventArgs e)
    {

        ImageButton button = (ImageButton)sender;
        Registro registro = button.BindingContext as Registro;
        if (registro != null)
        {
            string latitude = registro.latitud;
            string longitude = registro.longitud;

            string mapsUrl = $"https://www.google.com/maps?q={latitude},{longitude}";

            // Usar Launcher para abrir la URL en el navegador
            await Launcher.OpenAsync(new Uri(mapsUrl));
        }
        
    }
}