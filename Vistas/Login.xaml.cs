using proyectoFinal.Modelo;

namespace proyectoFinal.Vistas;

public partial class Login : ContentPage
{
    private Usuario usuario;
    public Login()
	{
		InitializeComponent();
        usuario = new Usuario();
    }

    private async void btn_login_Clicked(object sender, EventArgs e)
    {
        Usuario usu = await usuario.validarUsuario(txt_usuario.Text,txt_password.Text);
        if (usu != null)
        {
            DisplayAlert("Ëxito", $"Bienvenido {txt_usuario.Text}", "OK");
            Navigation.PushAsync(new HomeLogin());
        }
        else
        {
            DisplayAlert("Error", "El Usuario o la Contraseña son incorrectos", "OK");
            txt_usuario.Text = "";
            txt_password.Text = "";
        }
    }

    private void btn_registrar_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new RegistroLogin());

    }
}