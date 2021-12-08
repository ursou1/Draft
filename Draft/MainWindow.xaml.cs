using Draft.Views;
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

namespace Draft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow window;

        public MainWindow()
        {
            InitializeComponent();
            window = this;
            Navigate(new MaterialList());
        }

        public static void Navigate(Page page)
        {
            window.mainFrame.Navigate(page);
        }

        private void BackPage(object sender, RoutedEventArgs e)
        {
            if (mainFrame.CanGoBack)
                mainFrame.GoBack();
        }

        private void ForwardPage(object sender, RoutedEventArgs e)
        {
            if (mainFrame.CanGoForward)
                mainFrame.GoForward();
        }
    }
}
