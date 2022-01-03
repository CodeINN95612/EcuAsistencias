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


namespace EcuAsistencias.App.Views.Asistencia
{
    public sealed partial class AsistenciaDetalle : Page
    {
        private Frame Padre;
        private int Id;
        public AsistenciaDetalle(Frame padre, int id)
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
                grdAsistencia.DataContext = new AsistenciaViewModel();
                dteFecha.SelectedDate = DateTime.Today;
                dteIngreso.SelectedTime = DateTime.Today.TimeOfDay;
                dteSalida.SelectedTime = DateTime.Today.TimeOfDay;
            }
            else
            {
                AsistenciaViewModel asistencia = await AsistenciaService.GetAsistenciaAsync(Id);
                grdAsistencia.DataContext = asistencia;
                dteFecha.SelectedDate = asistencia.Fecha;
                dteIngreso.SelectedTime = asistencia.Ingreso.TimeOfDay;
                dteSalida.SelectedTime = asistencia.Salida?.TimeOfDay ?? TimeSpan.MinValue;
                txtId.IsReadOnly = true;
            }
        }
        private async Task CargarRequisitos()
        {
            List<UsuarioViewModel> usuarios = await UsuarioService.GetAllUsuariosAsync();
            cmdUsuario.ItemsSource = new ObservableCollection<UsuarioViewModel>(usuarios);
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Padre.Content = new AsistenciaListaPage(Padre);
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            AsistenciaViewModel asistencia = grdAsistencia.DataContext as AsistenciaViewModel;

            if (asistencia is null)
                return;

            asistencia.Fecha = dteFecha.Date.DateTime;
            asistencia.Ingreso = dteFecha.Date.DateTime + dteIngreso.Time;
            asistencia.Salida = dteFecha.Date.DateTime + dteSalida.Time;

            await AsistenciaService.GuardarAsync(asistencia);

            Padre.Content = new AsistenciaListaPage(Padre);
        }
    }
}
