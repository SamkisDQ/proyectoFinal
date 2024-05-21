using proyectoFinal.Modelo;

namespace proyectoFinal.Vistas;

public partial class BuscarRegistro : ContentPage
{
    private Registro registro;
    public BuscarRegistro()
	{
		InitializeComponent();
        registro = new Registro();
    }


    private async void btn_consultar_Clicked(object sender, EventArgs e)
    {
        listaRegistro.ItemsSource = await registro.obtenerPorCedula(txt_cedula.Text);

    }
}