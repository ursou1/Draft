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
        private string searchText = "";
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Search();
            }
        }

        #region for sort
        public List<string> FilterNum { get; set; }
        private string selectedFilterNum;
        private string selectedFilterMaterial;
        private string selectedFilterValue;
        public string SelectedFilterNum
        {
            get => selectedFilterNum;
            set
            {
                selectedFilterNum = value;
                Search();
            }
        }
        public List<string> FilterMaterial { get; set; }
        public string SelectedFilterMaterial
        {
            get => selectedFilterMaterial;
            set
            {
                selectedFilterMaterial = value;
                Search();
            }
        }
        public List<string> FilterValue { get; set; }
        public string SelectedFilterValue
        {
            get => selectedFilterValue;
            set
            {
                selectedFilterValue = value;
                Search();
            }
        }
        #endregion region

        private MaterialType selectedTypeFilter;
        public List<MaterialType> TypeFilter { get; set; }
        public MaterialType SelectedTypeFilter
        {
            get => selectedTypeFilter;
            set
            {
                selectedTypeFilter = value;
                Search();
            }
        }

        #region search
        private void Search()
        {
            var search = SearchText.ToLower();
            searchResult = DBInstance.Get().Material.Where(c => c.Title.ToLower().Contains(search) || c.Description.ToLower().Contains(search)).ToList();
            InitPagination();
            Pagination();
        }
        #endregion


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

            //lists for CB
            FilterNum = new List<string>();
            FilterMaterial = new List<string>();
            FilterValue = new List<string>();
            //заполнение комбобоксов
            FilterNum.AddRange(new string[] { "По умолчанию", "По увеличению", "По уменьшению" });
            FilterMaterial.AddRange(new string[] { "Все типы", "Рулон", "Гранулы", "Нарезка", "Пресс", });
            FilterValue.AddRange(new string[] { "По наименованию", "По остатку", "По стоимости" });
            //выбор первого значения
            selectedFilterNum = FilterNum.First();
            selectedFilterMaterial = FilterMaterial.First();
            selectedFilterValue = FilterValue.First();

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
