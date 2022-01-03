using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace EcuAsistencias.App.Views.PermisoSalida
{
    public sealed partial class PermisoSalidaDetalle : Page
    {
        private Frame Padre;
        private int Id;
        public PermisoSalidaDetalle(Frame padre, int id)
        {
            InitializeComponent();
            Padre = padre;
            Id = id;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarRequisitos();

            if (Id == 0)
            {
                grdPermisoSalida.DataContext = new PermisoSalidaViewModel();
                dteHoraPermiso.SelectedTime = DateTime.Today.TimeOfDay;
            }
            else
            {
                PermisoSalidaViewModel permisoSalida = await PermisoSalidaService.GetPermisoSalidaAsync(Id);
                grdPermisoSalida.DataContext = permisoSalida;
                dteHoraPermiso.SelectedTime = permisoSalida.HoraPermiso.TimeOfDay;
                txtTiempoPermisoHoras.Text = permisoSalida.TiempoPermisoHoras.ToString();
            }
        }
        private async Task CargarRequisitos()
        {
            List<AsistenciaViewModel> asistencias = await AsistenciaService.GetAllAsistenciasAsync();
            List<MotivoViewModel> motivos = await MotivoService.GetAllMotivosAsync();
            cmbAsistencias.ItemsSource = asistencias;
            cmbMotivo.ItemsSource = motivos;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Padre.Content = new PermisoSalidaListaPage(Padre);
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            PermisoSalidaViewModel permisoSalida = grdPermisoSalida.DataContext as PermisoSalidaViewModel;

            if (permisoSalida is null)
                return;

            permisoSalida.HoraPermiso = DateTime.Today + dteHoraPermiso.Time;
            permisoSalida.TiempoPermisoHoras = TimeSpan.Parse(txtTiempoPermisoHoras.Text);

            await PermisoSalidaService.GuardarAsync(permisoSalida);

            Padre.Content = new PermisoSalidaListaPage(Padre);
        }

        private void cmdMotivo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MotivoViewModel seleccionado = cmbMotivo.SelectedItem as MotivoViewModel;
            if(seleccionado.EsOtro)
            {
                panelMotivoOtros.Visibility = Visibility.Visible;
                txtMotivoOtros.Visibility = Visibility.Visible;
            }
            else
            {
                panelMotivoOtros.Visibility = Visibility.Collapsed;
                txtMotivoOtros.Visibility = Visibility.Collapsed;
            }
        }
    }
}
