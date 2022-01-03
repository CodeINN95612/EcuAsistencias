using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EcuAsistencias.App.Views.Usuario
{
    public sealed partial class UsuarioListaPage : Page
    {
        private Frame Padre;

        public UsuarioListaPage(Frame padre)
        {
            InitializeComponent();
            Padre = padre;
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<UsuarioViewModel> usuarios = await UsuarioService.GetAllUsuariosAsync();
            grid.ItemsSource = new ObservableCollection<UsuarioViewModel>(usuarios);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle("");
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UsuarioViewModel seleccionado = grid.SelectedItem as UsuarioViewModel;
            if(seleccionado != null)
                CargarDetalle(seleccionado.Identificacion);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UsuarioViewModel seleccionado = grid.SelectedItem as UsuarioViewModel;
            if (seleccionado != null)
                await UsuarioService.EliminarAsync(seleccionado);
            List<UsuarioViewModel> usuarios = await UsuarioService.GetAllUsuariosAsync();
            grid.ItemsSource = new ObservableCollection<UsuarioViewModel>(usuarios);
        }

        private void CargarDetalle(string identificacion)
        {
            Padre.Content = new UsuarioDetalle(Padre, identificacion);
        }
    }
}
