using proyectoFinal.Modelo;

namespace proyectoFinal.Vistas;

public partial class EditarEmpleado : ContentPage
{
	Empleados emp;

    public EditarEmpleado(Empleados empleado)
	{
		InitializeComponent();
        emp = empleado;
        txt_nombre.Text = empleado.nombre;
		txt_apellido.Text = empleado.apellido;
		txt_cedula.Text = empleado.cedula;
		txt_edad.Text = empleado.edad.ToString();
	}

    private async void btn_actualizar_Clicked(object sender, EventArgs e)
    {
        if (txt_nombre.Text != null || txt_apellido.Text != null || txt_cedula.Text != null || txt_edad.Text != null)
        {
            emp.nombre = txt_nombre.Text;
            emp.apellido = txt_apellido.Text;
            emp.cedula = txt_cedula.Text;
            emp.edad = Int32.Parse(txt_edad.Text);
            if (await emp.actualizarEmpleado(emp))
            {
                DisplayAlert("Ëxito", "Tu registro fue actualizado correctamente", "OK");
                Navigation.PushAsync(new HomeLogin());
            }
            else
            {
                DisplayAlert("Error", "Vuelve a intentar", "OK");

            }

        }

    }
}