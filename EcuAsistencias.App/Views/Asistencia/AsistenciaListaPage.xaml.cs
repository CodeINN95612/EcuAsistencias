using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EcuAsistencias.App.Views.Asistencia
{
    public sealed partial class AsistenciaListaPage : Page
    {
        private Frame Padre;

        public AsistenciaListaPage(Frame padre)
        {
            InitializeComponent();
            Padre = padre;
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<AsistenciaDto> asistencias = await AsistenciaService.GetAllAsistenciasAsync();
            grid.ItemsSource = new ObservableCollection<AsistenciaDto>(asistencias);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle(0);
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AsistenciaDto seleccionado = grid.SelectedItem as AsistenciaDto;
            if (seleccionado != null)
                CargarDetalle(seleccionado.Id);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AsistenciaDto seleccionado = grid.SelectedItem as AsistenciaDto;
            if (seleccionado != null)
                await AsistenciaService.EliminarAsync(seleccionado);

            List<AsistenciaDto> asistencias = await AsistenciaService.GetAllAsistenciasAsync();
            grid.ItemsSource = new ObservableCollection<AsistenciaDto>(asistencias);
        }

        private void CargarDetalle(int id)
        {
            Padre.Content = new AsistenciaDetalle(Padre, id);
        }
    }
}
