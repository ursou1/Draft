using Draft.DB;
using Draft.Views;
using System;
using System.Collections.Generic;
using System.IO;
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
            
            #region add

            #region добавление mateial
            //var connection = DBInstance.Get();
            //string path = @"C:\Users\ur_soul\Desktop\Произв. практика\Черновик\Сессия 1\materials_b_import.csv";
            //var rows = File.ReadAllLines(path);
            //var materials = connection.Material.ToList();
            //var materialsType = connection.MaterialType.ToList();
            //var materialsInStock = connection.MaterialCountHistory.ToList();
            //connection.MaterialType.Add(new MaterialType { Title = "Рулон" });
            //connection.MaterialType.Add(new MaterialType { Title = "Гранулы" });
            //connection.MaterialType.Add(new MaterialType { Title = "Нарезка" });
            //connection.MaterialType.Add(new MaterialType { Title = "Пресс" });

            //for (int i = 1; i < rows.Length; i++)
            //{
            //    var cols = rows[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //    var material = materials.FirstOrDefault(s => s.ID.ToString() == cols[0]);

            //    connection.Material.Add(new Material
            //    {
            //        Title = cols[0],
            //        MaterialType = materialsType.First(s => s.Title == cols[1]),
            //        Image = cols[2],
            //        Cost = decimal.Parse(cols[3]),
            //        CountInStock = int.Parse(cols[4]),
            //        MinCount = int.Parse(cols[5]),
            //        CountInPack = int.Parse(cols[6]),
            //        Unit = cols[7]
            //    });
            //}
            //connection.SaveChanges();
            #endregion

            #region добавление supplier
            //var connection = DBInstance.Get();
            //string path = @"C:\Users\ur_soul\Desktop\Произв. практика\Черновик\Сессия 1\supplier_b_import.txt";
            //var rows = File.ReadAllLines(path);
            //var supplier = connection.Supplier.ToList();

            //for (int i = 1; i < rows.Length; i++)
            //{
            //    var cols = rows[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //    connection.Supplier.Add(new Supplier
            //    {
            //        Title = cols[0],
            //        SupplierType = cols[1],
            //        INN = cols[2],
            //        QualityRating = int.Parse(cols[3]),
            //        StartDate = DateTime.Parse(cols[4])
            //    });
            //}

            //connection.SaveChanges();
            #endregion

            #region добавление material supplier
            //var connection = DBInstance.Get();
            //string path = @"C:\Users\ur_soul\Desktop\Произв. практика\Черновик\Сессия 1\materialsupplier_b_import.csv";
            //var rows = File.ReadAllLines(path);
            //var supliers = connection.Supplier.ToList();
            //var materials = connection.Material.ToList();

            //for (int i = 1; i < rows.Length; i++)
            //{
            //    var cols = rows[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //    var material = materials.First(s => s.Title == cols[0]);
            //    var suplier = supliers.First(s => s.Title == cols[1]);
            //    suplier.Material.Add(material);
            //}
            //connection.SaveChanges();
            #endregion


            #endregion
            
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



        //это пока в помойку
        #region add
        //var connection = DBInstance.Get();
        //string path = @"C:\Users\79249\Desktop\Черновик\Сессия 1\materials_b_import.csv";
        //var rows = File.ReadAllLines(path);
        //var materials = connection.Material.ToList();
        //var materialsType = connection.MaterialType.ToList();
        //var materialsInStock = connection.MaterialCountHistory.ToList();
        ////connection.MaterialType.Add(new MaterialType { Title = "Рулон" });
        ////connection.MaterialType.Add(new MaterialType { Title = "Гранулы" });
        ////connection.MaterialType.Add(new MaterialType { Title = "Нарезка" });
        ////connection.MaterialType.Add(new MaterialType { Title = "Пресс" });

        //for (int i = 1; i < rows.Length; i++)
        //{
        // var cols = rows[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        // var material = materials.FirstOrDefault(s => s.ID.ToString() == cols[0]);

        // connection.Material.Add(new Material
        // {
        // Title = cols[0],
        // MaterialType = materialsType.First(s => s.Title == cols[1]),
        // Image = cols[2],
        // Cost = decimal.Parse(cols[3]),
        // CountInStock = int.Parse(cols[4]),
        // MinCount = int.Parse(cols[5]),
        // CountInPack = int.Parse(cols[6]),
        // Unit = cols[7]
        // });
        //}
        //connection.SaveChanges();
        //var connection = DBInstance.Get();
        //string path = @"C:\Users\79249\Desktop\Черновик\Сессия 1\supplier_b_import.txt";
        //var rows = File.ReadAllLines(path);
        //var supplier = connection.Supplier.ToList();

        //for (int i = 1; i < rows.Length; i++)
        //{
        // var cols = rows[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        // connection.Supplier.Add(new Supplier
        // {
        // Title = cols[0],
        // SupplierType = cols[1],
        // INN = cols[2],
        // QualityRating = int.Parse(cols[3]),
        // StartDate = DateTime.Parse(cols[4])
        // });
        //}

        //connection.SaveChanges();

        // var connection = DBInstance.Get();
        // string path = @"C:\Users\79249\Desktop\Черновик\Сессия 1\materialsupplier_b_import.csv";
        // var rows = File.ReadAllLines(path);
        // var supliers = connection.Supplier.ToList();
        // var materials = connection.Material.ToList();

        // for (int i = 1; i < rows.Length; i++)
        // {
        // var cols = rows[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        // var material = materials.First(s => s.Title == cols[0]);
        // var suplier = supliers.First(s => s.Title == cols[1]);
        // suplier.Material.Add(material);
        // }
        // connection.SaveChanges();

        #endregion


    }
}
