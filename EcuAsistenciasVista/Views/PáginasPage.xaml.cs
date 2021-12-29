using System;

using EcuAsistenciasVista.ViewModels;

using Windows.UI.Xaml.Controls;

namespace EcuAsistenciasVista.Views
{
    public sealed partial class PáginasPage : Page
    {
        public PáginasViewModel ViewModel { get; } = new PáginasViewModel();

        public PáginasPage()
        {
            InitializeComponent();
        }
    }
}
