using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EcuAsistencias.App.Views.Rol
{
    public sealed partial class RolDetalle : Page
    {
        private Frame Padre;
        private int Id;
        public RolDetalle(Frame padre, int id)
        {
            InitializeComponent();
            Padre = padre;
            Id = id;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Id == 0)
            {
                grdRol.DataContext = new RolViewModel();
                txtId.Visibility = Visibility.Collapsed;
            }
            else
            {
                grdRol.DataContext = await RolService.GetRolAsync(Id);
                txtId.Visibility = Visibility.Visible;
                txtId.IsReadOnly = true;
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Padre.Content = new RolListaPage(Padre);
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            RolViewModel rol = grdRol.DataContext as RolViewModel;

            if (rol is null)
                return;

            await RolService.GuardarAsync(rol);

            Padre.Content = new RolListaPage(Padre);
        }
    }
}
