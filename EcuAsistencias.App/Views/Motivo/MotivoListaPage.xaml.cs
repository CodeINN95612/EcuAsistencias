using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EcuAsistencias.App.ViewModels;
using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.ViewModels;
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
            List<MotivoViewModel> usuarios = await MotivoService.GetAllMotivosAsync();
            grid.ItemsSource = new ObservableCollection<MotivoViewModel>(usuarios);
        }
        private void btnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CargarDetalle(0);
        }

        private void btnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MotivoViewModel seleccionado = grid.SelectedItem as MotivoViewModel;
            if (seleccionado != null)
                CargarDetalle(seleccionado.Id);
        }

        private async void btnBorrar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MotivoViewModel seleccionado = grid.SelectedItem as MotivoViewModel;
            if (seleccionado != null)
                await MotivoService.EliminarAsync(seleccionado);

            List<MotivoViewModel> motivoes = await MotivoService.GetAllMotivosAsync();
            grid.ItemsSource = new ObservableCollection<MotivoViewModel>(motivoes);
        }

        private void CargarDetalle(int id)
        {
            Padre.Content = new MotivoDetalle(Padre, id);
        }
    }
}
