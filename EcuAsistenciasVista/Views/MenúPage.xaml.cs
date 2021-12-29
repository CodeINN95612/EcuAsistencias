using System;

using EcuAsistenciasVista.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EcuAsistenciasVista.Views
{
    public sealed partial class MenúPage : Page
    {
        public MenúViewModel ViewModel { get; } = new MenúViewModel();

        public MenúPage()
        {
            InitializeComponent();
            Loaded += MenúPage_Loaded;
        }

        private async void MenúPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
