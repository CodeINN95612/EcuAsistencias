using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EcuAsistencias.App.Dtos;
using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.Dtos;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EcuAsistencias.App.Views.Motivo
{
    public sealed partial class MotivoListaPage : Page
    {
        private Frame Padre;

        public MotivoListaPage(Frame padre)
        {
            InitializeComponent();
            Padre = padre;
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<MotivoDto> usuarios = await MotivoService.GetAllMotivosAsync();
            grid.ItemsSource = new ObservableCollection<MotivoDto>(usuarios);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle(0);
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MotivoDto seleccionado = grid.SelectedItem as MotivoDto;
            if (seleccionado != null)
                CargarDetalle(seleccionado.Id);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MotivoDto seleccionado = grid.SelectedItem as MotivoDto;
            if (seleccionado != null)
                await MotivoService.EliminarAsync(seleccionado);

            List<MotivoDto> motivoes = await MotivoService.GetAllMotivosAsync();
            grid.ItemsSource = new ObservableCollection<MotivoDto>(motivoes);
        }

        private void CargarDetalle(int id)
        {
            Padre.Content = new MotivoDetalle(Padre, id);
        }
    }
}
