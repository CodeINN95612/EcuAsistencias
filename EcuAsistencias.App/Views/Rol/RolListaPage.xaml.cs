using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EcuAsistencias.App.Views.Rol
{
    public sealed partial class RolListaPage : Page
    {
        private Frame Padre;

        public RolListaPage(Frame padre)
        {
            InitializeComponent();
            Padre = padre;
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<RolViewModel> usuarios = await RolService.GetAllRolesAsync();
            grid.ItemsSource = new ObservableCollection<RolViewModel>(usuarios);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle(0);
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RolViewModel seleccionado = grid.SelectedItem as RolViewModel;
            if (seleccionado != null)
                CargarDetalle(seleccionado.Id);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RolViewModel seleccionado = grid.SelectedItem as RolViewModel;
            if (seleccionado != null)
                await RolService.EliminarAsync(seleccionado);

            List<RolViewModel> roles = await RolService.GetAllRolesAsync();
            grid.ItemsSource = new ObservableCollection<RolViewModel>(roles);
        }

        private void CargarDetalle(int id)
        {
            Padre.Content = new RolDetalle(Padre, id);
        }
    }
}
