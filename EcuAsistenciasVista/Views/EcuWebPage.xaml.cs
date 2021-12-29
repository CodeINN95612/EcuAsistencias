using System;

using EcuAsistenciasVista.ViewModels;

using Windows.UI.Xaml.Controls;

namespace EcuAsistenciasVista.Views
{
    public sealed partial class EcuWebPage : Page
    {
        public EcuWebViewModel ViewModel { get; } = new EcuWebViewModel();

        public EcuWebPage()
        {
            InitializeComponent();
            ViewModel.Initialize(webView);
        }
    }
}
