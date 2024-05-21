using proyectoFinal.Modelo;

namespace proyectoFinal.Vistas;

public partial class NuevoEmpleado : ContentPage
{
    Empleados empleado;

    public NuevoEmpleado()
	{
		InitializeComponent();
        empleado=new Empleados();

    }

    private async void btn_registrar_Clicked(object sender, EventArgs e)
    {
        if (txt_nombre.Text != null || txt_apellido.Text != null || txt_cedula.Text != null || txt_edad.Text != null)
        {

            if (await empleado.ObtenerEmpleadoPorCedula(txt_cedula.Text)!=null)
            {
                DisplayAlert("Error", "Ya existe un empleado con la cedula ingresada.", "OK");

            }
            else
            {
                Empleados empleadoNew = new Empleados();
                empleadoNew.nombre = txt_nombre.Text;
                empleadoNew.apellido = txt_apellido.Text;
                empleadoNew.cedula = txt_cedula.Text;
                empleadoNew.edad = Int32.Parse(txt_edad.Text);
                if (empleado.insertarEmpleado(empleadoNew))
                {
                    DisplayAlert("Ëxito", "Tu registro fue ingresado correctamente", "OK");
                    txt_nombre.Text = "";
                    txt_apellido.Text = "";
                    txt_cedula.Text = "";
                    txt_edad.Text = "";
                    Navigation.PushAsync(new HomeLogin());
                }
                else
                {
                    DisplayAlert("Error", "Vuelve a intentar", "OK");

                }

            }
        }
        else
        {
            DisplayAlert("Error", "Existen campos vacios.", "OK");

        }

    }

}