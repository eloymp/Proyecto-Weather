using Weather.Modelo;
using Weather.Singleton;

namespace Weather.Vista;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
	}

    private void Guardar_Clicked(object sender, EventArgs e)
    {
        // Lógica para manejar el evento de clic del botón "Guardar".
        GlobalData.Instance.miBBDD.InsertarUsuario(User.Text, Password.Text);
        User.Text = "";
        Password.Text = "";
    }

    private void Salir_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage=new Navegacion.NavegacionInicial();
    }


}