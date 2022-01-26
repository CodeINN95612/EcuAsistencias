using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EcuAsistencias.App.Views.PermisoSalida
{
    public sealed partial class PermisoSalidaListaPage : Page
    {
        private Frame Padre;

        public PermisoSalidaListaPage(Frame padre)
        {
            InitializeComponent();
            Padre = padre;
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<PermisoSalidaDto> usuarios = await PermisoSalidaService.GetAllPermisosSalidaAsync();
            grid.ItemsSource = new ObservableCollection<PermisoSalidaDto>(usuarios);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle(0);
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PermisoSalidaDto seleccionado = grid.SelectedItem as PermisoSalidaDto;
            if (seleccionado != null)
                CargarDetalle(seleccionado.Id);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PermisoSalidaDto seleccionado = grid.SelectedItem as PermisoSalidaDto;
            if (seleccionado != null)
                await PermisoSalidaService.EliminarAsync(seleccionado);

            List<PermisoSalidaDto> roles = await PermisoSalidaService.GetAllPermisosSalidaAsync();
            grid.ItemsSource = new ObservableCollection<PermisoSalidaDto>(roles);
        }

        private void CargarDetalle(int id)
        {
            Padre.Content = new PermisoSalidaDetalle(Padre, id);
        }
    }
}
