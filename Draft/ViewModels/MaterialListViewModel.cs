using Draft.DB;
using Draft.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draft.ViewModels
{
    public class MaterialListViewModel : BaseViewModel
    {
        private List<Material> materials;
        public List<Material> Materials
        {
            get => materials;
            set
            {
                materials = value;
                SignalChanged();
            }
        }
        private List<MaterialType> materialTypes;
        public List<MaterialType> MaterialTypes
        {
            get => materialTypes;
            set
            {
                materialTypes = value;
                SignalChanged();
            }
        }
        private List<Supplier> suppliers;
        public List<Supplier> Suppliers
        {
            get => suppliers;
            set
            {
                suppliers = value;
                SignalChanged();
            }
        }

        private string supplierString = "453545";
        public string SuppliersString
        {
            get => supplierString;
            set
            {
                supplierString = value;
                SignalChanged();
            }
        }
        public List<string> ViewCountRows { get; set; }
        public string SelectedViewCountRows
        {
            get => selectedViewCountRows;
            set
            {
                selectedViewCountRows = value;
                paginationPageIndex = 0;
                Pagination();
            }
        }
        public string SearchCountRows
        {
            get => searchCountRows;
            set
            {
                searchCountRows = value;
                SignalChanged();
            }
        }

        public CustomCommand BackPage { get; set; }
        public CustomCommand ForwardPage { get; set; }

        List<Material> searchResult;
        int paginationPageIndex = 0;
        private string searchCountRows;
        private string selectedViewCountRows;
        public MaterialListViewModel()
        {
            var connection = DBInstance.Get();

            Materials = new List<Material>(connection.Material.ToList());
            MaterialTypes = new List<MaterialType>(connection.MaterialType.ToList());
            Suppliers = new List<Supplier>(connection.Supplier.ToList());

            /*
            foreach (var image in Materials)
            {
                if (image.Image == "отсутствует")
                {
                    image.Image = @"\Images\picture.png";
                    SignalChanged("Materials");
                }
            }
            connection.SaveChanges();
            */

            ViewCountRows = new List<string>();
            ViewCountRows.AddRange(new string[] { "15", "все" });
            selectedViewCountRows = ViewCountRows.First();

            BackPage = new CustomCommand(() =>
            {
                if (searchResult == null)
                    return;
                if (paginationPageIndex > 0)
                    paginationPageIndex--;
                Pagination();
            });

            ForwardPage = new CustomCommand(() =>
            {
                if (searchResult == null)
                    return;
                int.TryParse(SelectedViewCountRows, out int rowsOnPage);
                if (rowsOnPage == 0)
                    return;
                int countPage = searchResult.Count() / rowsOnPage;
                if (searchResult.Count() % rowsOnPage != 0)
                    countPage++;
                if (countPage > paginationPageIndex + 1)
                    paginationPageIndex++;
                Pagination();

            });
            searchResult = DBInstance.Get().Material.ToList();
            InitPagination();
            Pagination();
        }
        private void InitPagination()
        {
            SearchCountRows = $"Найдено записей: {searchResult.Count} из {DBInstance.Get().Material.Count()}";
            paginationPageIndex = 0;
        }

        private void Pagination()
        {
            int rowsOnPage = 0;
            if (!int.TryParse(SelectedViewCountRows, out rowsOnPage))
            {
                Materials = searchResult;
            }
            else
            {
                Materials = searchResult.Skip(rowsOnPage * paginationPageIndex)
                    .Take(rowsOnPage).ToList();
            }
        }


    }
}
