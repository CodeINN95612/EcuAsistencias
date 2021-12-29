using System;

using EcuAsistenciasVista.ViewModels;

using Windows.UI.Xaml.Controls;

namespace EcuAsistenciasVista.Views
{
    public sealed partial class RegistroPage : Page
    {
        public RegistroViewModel ViewModel { get; } = new RegistroViewModel();

        public RegistroPage()
        {
            InitializeComponent();
        }
    }
}
