using Draft.DB;
using Draft.ViewModels;
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

namespace Draft.Views
{
    /// <summary>
    /// Логика взаимодействия для EditMaterial.xaml
    /// </summary>
    public partial class EditMaterial : Window
    {
        
        public EditMaterial()
        {
            InitializeComponent();
            DataContext = new EditMaterialViewModel(null);
        }
        public EditMaterial(Material material)
        {
            InitializeComponent();
            DataContext = new EditMaterialViewModel(material);
        }
    }
}
