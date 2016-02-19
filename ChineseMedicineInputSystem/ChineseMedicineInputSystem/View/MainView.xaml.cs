using ChineseMedicineInputSystem.ViewModel;
using System.Windows.Controls;

namespace ChineseMedicineInputSystem.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = MainViewModel.Create();
        }
    }
}
