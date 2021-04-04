using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace temperature_conversion_app.Views
{
    /// <summary>
    /// Interaction logic for ConversionWindow.xaml
    /// </summary>
    public partial class ConversionWindow : Window
    {
        public ConversionWindow(Models.User user)
        {
            InitializeComponent();
            DataContext = new ViewModels.ConversionViewModel(this, user);
        }
    }
}
