using System.Windows;

namespace FitnessDK
{
    /// <summary>
    ///     Interaction logic for Opdater.xaml
    /// </summary>
    public partial class Opdater : Window
    {
        private readonly CustomerViewModel _CustomViewModel;

        public Opdater(ref CustomerViewModel CustomViewModel)
        {
            _CustomViewModel = CustomViewModel;
            DataContext = _CustomViewModel;

            InitializeComponent();
        }
    }
}