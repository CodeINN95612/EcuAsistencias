using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EcuAsistencias.App.Views.Usuario
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class UsuarioMainPage : Page
    {
        public UsuarioMainPage()
        {
            this.InitializeComponent();
            frameContenido.Content = new UsuarioListaPage(frameContenido);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}
