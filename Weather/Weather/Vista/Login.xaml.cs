using System.Data;
using Weather.Singleton;

namespace Weather.Vista;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //GlobalData.Instance.Usuario = user.Text;
        DataTable resultadoConsulta = GlobalData.Instance.miBBDD.ConsultarUser(user.Text);

        //if (user.Text != "" && password.Text != "" && user.Text != null && password.Text != null)
        if (!string.IsNullOrWhiteSpace(user.Text) && !string.IsNullOrWhiteSpace(password.Text))
        {
            if (resultadoConsulta != null && resultadoConsulta.Rows.Count != 0 && resultadoConsulta.Rows[0]["Name"].ToString() == user.Text && resultadoConsulta.Rows[0]["Password"].ToString() == password.Text)
            {
                if (resultadoConsulta.Rows[0]["Rol"].ToString() == "admin")
                {
                    Application.Current.MainPage = new Navegacion.NavegacionAdministrator();
                }
                else
                {
                    Application.Current.MainPage = new Navegacion.NavegacionUsuario();
                }
            }
            else
            {
                DisplayAlert("Error", "Usuario o contraseña incorrecto", "Aceptar");
            }
        }
        else
        {
            DisplayAlert("Error","Usuario o contraseña vacios", "Aceptar");
        }
    }
}


