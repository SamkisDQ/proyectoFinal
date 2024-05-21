
using proyectoFinal.Modelo;
using System.Timers;
using Timer = System.Timers.Timer;


namespace proyectoFinal.Vistas;

public partial class Home : ContentPage
{
    private Timer timer;

    private Registro registro;
    private Empleados empleado;

    public Home()
    {
        InitializeComponent();
        StartTimer();
        registro = new Registro();
        empleado = new Empleados();
        obtener();
    }

    private void StartTimer()
    {
        timer = new Timer(1000); // Set the timer to tick every 1000 milliseconds (1 second)
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        // Update the label on the main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            DateTime now = DateTime.Now;
            string fechaEnPalabras = now.ToString("dddd, dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));
            string hora = now.ToString("T");
            DateLabel.Text = fechaEnPalabras;
            TimeLabel.Text = hora;
        });
    }

    public async void obtener()
    {
        listaRegistro.ItemsSource = await registro.obtener();

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Stop the timer when the page is not visible
        timer.Stop();
        timer.Dispose();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartTimer();


    }



    private void btn_buscar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BuscarRegistro());
    }

    private void btn_login_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login());
    }

    private async void btn_timbrar_Clicked(object sender, EventArgs e)
    {

        Empleados emp = await empleado.ObtenerEmpleadoPorCedula(txt_cedula.Text);
        if (emp != null)
        {
            try
            {
                loadingOverlay.IsVisible = true;
                var ubicacion = await ObtenerUbicacionActual();
                Registro registroNew = new Registro();
                registroNew.idEmpleado = emp.id;
                registroNew.latitud = ubicacion.Latitude.ToString().Replace(',', '.');
                registroNew.longitud = ubicacion.Longitude.ToString().Replace(',', '.');
                registroNew.empleado = emp;

                if (registro.insertarRegistro(registroNew))
                {
                    DisplayAlert("Ëxito", "Tu registro fue ingresado correctamente", "OK");
                    txt_cedula.Text = "";
                    obtener();
                }
                else
                {
                    DisplayAlert("Error", "Has superado tu registro de asistencia por el dia de Hoy", "OK");
                }
            }
            finally
            {
                loadingOverlay.IsVisible = false;
            }
        }
        else
        {
            DisplayAlert("Error", "Cédula No Encontrada", "OK");
        }
    }

    public async Task<Location> ObtenerUbicacionActual()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(30));
            var location = await Geolocation.GetLocationAsync(request, CancellationToken.None);

            if (location != null)
            {
                Console.WriteLine($"Ubicación actual: Latitude: {location.Latitude}, Longitude: {location.Longitude}");
            }

            return location;
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // La funcionalidad de geolocalización no es soportada en este dispositivo
            Console.WriteLine($"Error: {fnsEx.Message}");
            return null;
        }
        catch (PermissionException pEx)
        {
            // Permiso denegado para acceder a la ubicación
            Console.WriteLine($"Error: {pEx.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Otro tipo de error
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}