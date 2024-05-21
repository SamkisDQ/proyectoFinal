
using proyectoFinal.Modelo;

namespace proyectoFinal.Vistas;

public partial class HomeLogin : ContentPage
{
    private Empleados empleado;
    public HomeLogin()
	{
		InitializeComponent();
        empleado = new Empleados();
        obtenerAll();

    }

    public async void obtenerAll()
    {
        Empleados empl = new Empleados();
        listaEmpleados.ItemsSource = await empl.obtenerAll();

    }


    private async void txt_cedula_TextChanged(object sender, TextChangedEventArgs e)
    {
        //       DisplayAlert("Error", "El Usuario o la Contraseña son incorrectos", "OK");

        listaEmpleados.ItemsSource = await empleado.obtenerPorCedulaAll(txt_cedula.Text);

    }

    private void btn_agregarEmpleado_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NuevoEmpleado());

    }

    private void btn_editarEmpleado_Clicked(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        Empleados empleados = button.BindingContext as Empleados;
        if (empleados != null)
        {
            Navigation.PushAsync(new EditarEmpleado(empleados));
        }
    }

    private void btn_editarRegistro_Clicked(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        Empleados empleados = button.BindingContext as Empleados;
        if (empleados != null)
        {
            Navigation.PushAsync(new VerRegistro(empleados));
        }
    }
}