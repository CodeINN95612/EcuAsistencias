using System;

using EcuAsistenciasVista.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EcuAsistenciasVista.Views
{
    public sealed partial class MenúDetailControl : UserControl
    {
        public SampleOrder ListMenuItem
        {
            get { return GetValue(ListMenuItemProperty) as SampleOrder; }
            set { SetValue(ListMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListMenuItemProperty = DependencyProperty.Register("ListMenuItem", typeof(SampleOrder), typeof(MenúDetailControl), new PropertyMetadata(null, OnListMenuItemPropertyChanged));

        public MenúDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MenúDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
