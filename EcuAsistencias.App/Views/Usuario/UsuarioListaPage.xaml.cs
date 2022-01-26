using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.Dtos;
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
            List<UsuarioDto> usuarios = await UsuarioService.GetAllUsuariosAsync();
            grid.ItemsSource = new ObservableCollection<UsuarioDto>(usuarios);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle("");
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UsuarioDto seleccionado = grid.SelectedItem as UsuarioDto;
            if(seleccionado != null)
                CargarDetalle(seleccionado.Identificacion);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UsuarioDto seleccionado = grid.SelectedItem as UsuarioDto;
            if (seleccionado != null)
                await UsuarioService.EliminarAsync(seleccionado);
            List<UsuarioDto> usuarios = await UsuarioService.GetAllUsuariosAsync();
            grid.ItemsSource = new ObservableCollection<UsuarioDto>(usuarios);
        }

        private void CargarDetalle(string identificacion)
        {
            Padre.Content = new UsuarioDetalle(Padre, identificacion);
        }
    }
}
