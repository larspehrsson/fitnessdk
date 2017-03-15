using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnessDK
{
    /// <summary>
    /// Interaction logic for HoldChangesWindow.xaml
    /// </summary>
    public partial class HoldChangesWindow : Window
    {
        private readonly CustomerViewModel _CustomViewModel;

        public HoldChangesWindow(ref CustomerViewModel CustomViewModel)
        {
            _CustomViewModel = CustomViewModel;
            DataContext = _CustomViewModel;

            InitializeComponent();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
