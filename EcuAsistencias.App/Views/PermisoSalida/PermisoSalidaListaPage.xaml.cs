using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.ViewModels;
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
            List<PermisoSalidaViewModel> usuarios = await PermisoSalidaService.GetAllPermisosSalidaAsync();
            grid.ItemsSource = new ObservableCollection<PermisoSalidaViewModel>(usuarios);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle(0);
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PermisoSalidaViewModel seleccionado = grid.SelectedItem as PermisoSalidaViewModel;
            if (seleccionado != null)
                CargarDetalle(seleccionado.Id);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PermisoSalidaViewModel seleccionado = grid.SelectedItem as PermisoSalidaViewModel;
            if (seleccionado != null)
                await PermisoSalidaService.EliminarAsync(seleccionado);

            List<PermisoSalidaViewModel> roles = await PermisoSalidaService.GetAllPermisosSalidaAsync();
            grid.ItemsSource = new ObservableCollection<PermisoSalidaViewModel>(roles);
        }

        private void CargarDetalle(int id)
        {
            Padre.Content = new PermisoSalidaDetalle(Padre, id);
        }
    }
}
