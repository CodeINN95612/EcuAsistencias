using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.Dtos;
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
            List<RolDto> usuarios = await RolService.GetAllRolesAsync();
            grid.ItemsSource = new ObservableCollection<RolDto>(usuarios);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle(0);
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RolDto seleccionado = grid.SelectedItem as RolDto;
            if (seleccionado != null)
                CargarDetalle(seleccionado.Id);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RolDto seleccionado = grid.SelectedItem as RolDto;
            if (seleccionado != null)
                await RolService.EliminarAsync(seleccionado);

            List<RolDto> roles = await RolService.GetAllRolesAsync();
            grid.ItemsSource = new ObservableCollection<RolDto>(roles);
        }

        private void CargarDetalle(int id)
        {
            Padre.Content = new RolDetalle(Padre, id);
        }
    }
}
