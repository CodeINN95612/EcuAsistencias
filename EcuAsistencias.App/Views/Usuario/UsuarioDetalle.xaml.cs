using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace EcuAsistencias.App.Views.Usuario
{
    public sealed partial class UsuarioDetalle : Page
    {
        private Frame Padre;
        private string Identificacion;
        public UsuarioDetalle(Frame padre, string identificacion)
        {
            InitializeComponent();
            Padre = padre;
            Identificacion = identificacion;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarRequisitos();

            if(Identificacion == "")
            {
                grdUsuario.DataContext = new UsuarioViewModel();
                dteNacimiento.SelectedDate = DateTime.Today;
            }
            else
            {
                UsuarioViewModel usuario = await UsuarioService.GetUsuarioAsync(Identificacion);
                grdUsuario.DataContext = usuario;
                dteNacimiento.SelectedDate = DateTime.Today;
                txtIdentificacion.IsReadOnly = true;
            }
        }
        private async Task CargarRequisitos()
        {
            List<RolViewModel> roles = await RolService.GetAllRolesAsync();
            cmbRol.ItemsSource = new ObservableCollection<RolViewModel>(roles);
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Padre.Content = new UsuarioListaPage(Padre);
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            UsuarioViewModel usuario = grdUsuario.DataContext as UsuarioViewModel;
            usuario.FechaNacimiento = dteNacimiento.Date.DateTime;
            usuario.HorarioInicio = DateTime.Today + tmInicio.Time;
            usuario.HorarioFin = DateTime.Today + tmFin.Time;

            if (usuario is null)
                return;

            if (string.IsNullOrEmpty(Identificacion))
                await UsuarioService.CrearAsync(usuario);
            else
                await UsuarioService.EditarAsync(usuario);

            Padre.Content = new UsuarioListaPage(Padre);
        }
    }
}
