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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MultiViewApp.ViewModel;

namespace MultiViewApp.View
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View2 : UserControl
    {
        public View2()
        {
            InitializeComponent();

            DataContext = new View2_ViewModel();

        }
        // added automa:
        public View2_ViewModel DataContext { get; }
    }
}