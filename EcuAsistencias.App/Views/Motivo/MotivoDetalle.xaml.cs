using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
namespace EcuAsistencias.App.Views.Motivo
{
    public sealed partial class MotivoDetalle : Page
    {
        private Frame Padre;
        private int Id;
        public MotivoDetalle(Frame padre, int id)
        {
            InitializeComponent();
            Padre = padre;
            Id = id;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Id == 0)
            {
                grdMotivo.DataContext = new MotivoViewModel();
                txtId.Visibility = Visibility.Collapsed;
            }
            else
            {
                grdMotivo.DataContext = await MotivoService.GetMotivoAsync(Id);
                txtId.Visibility = Visibility.Visible;
                txtId.IsReadOnly = true;
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Padre.Content = new MotivoListaPage(Padre);
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            MotivoViewModel motivo = grdMotivo.DataContext as MotivoViewModel;

            if (motivo is null)
                return;

            await MotivoService.GuardarAsync(motivo);

            Padre.Content = new MotivoListaPage(Padre);
        }
    }
}
