using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


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
                grdAsistencia.DataContext = new AsistenciaDto();
                dteFecha.SelectedDate = DateTime.Today;
                dteIngreso.SelectedTime = DateTime.Today.TimeOfDay;
                dteSalida.SelectedTime = DateTime.Today.TimeOfDay;
            }
            else
            {
                AsistenciaDto asistencia = await AsistenciaService.GetAsistenciaAsync(Id);
                grdAsistencia.DataContext = asistencia;
                dteFecha.SelectedDate = asistencia.Fecha;
                dteIngreso.SelectedTime = asistencia.Ingreso.TimeOfDay;
                dteSalida.SelectedTime = asistencia.Salida?.TimeOfDay ?? TimeSpan.MinValue;
                txtId.IsReadOnly = true;
            }
        }
        private async Task CargarRequisitos()
        {
            List<UsuarioDto> usuarios = await UsuarioService.GetAllUsuariosAsync();
            cmdUsuario.ItemsSource = new ObservableCollection<UsuarioDto>(usuarios);
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Padre.Content = new AsistenciaListaPage(Padre);
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            AsistenciaDto asistencia = grdAsistencia.DataContext as AsistenciaDto;

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
