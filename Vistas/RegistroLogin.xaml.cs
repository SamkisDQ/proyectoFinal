using proyectoFinal.Modelo;

namespace proyectoFinal.Vistas;

public partial class RegistroLogin : ContentPage
{
    Usuario usuario;
	public RegistroLogin()
	{
		InitializeComponent();
        usuario = new Usuario();
	}

    private async void btn_registrar_Clicked(object sender, EventArgs e)
    {
        if (txt_usuario.Text!=null || txt_password.Text != null || txt_password2.Text != null) { 

            if (await usuario.existeUsuario(txt_usuario.Text))
            {
                DisplayAlert("Error", "El Usuario ingresado ya esta en uso.", "OK");

            }
            else {
                if (txt_password.Text==txt_password2.Text) { 
                    Usuario usuarioNew = new Usuario();
                    usuarioNew.usuario = txt_usuario.Text;
                    usuarioNew.password = txt_password.Text;
                    if (usuario.insertarUsuario(usuarioNew))
                    {
                        DisplayAlert("Ëxito", "Tu registro fue ingresado correctamente", "OK");
                        txt_usuario.Text = "";
                        txt_password.Text = "";
                        txt_password2.Text = "";
                    }
                    else
                    {
                        DisplayAlert("Error", "Vuelve a intentar", "OK");

                    }
                }
                else
                {
                    DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");

                }
            }
        }
        else
        {
            DisplayAlert("Error", "Existen campos vacios.", "OK");

        }
    }

    private void btn_regresar_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new Login());
    }
}