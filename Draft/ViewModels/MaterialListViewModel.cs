using Draft.DB;
using Draft.Tools;
using Draft.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

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
        private Material selectedMaterial;
        public Material SelectedMaterial
        {
            get => selectedMaterial;
            set
            {
                selectedMaterial = value;
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
        public List<MaterialType> MaterialTypeFilter { get; set; }
        public MaterialType SelectedMaterialTypeFilter
        {
            get => selectedMaterialTypeFilter;
            set
            {
                selectedMaterialTypeFilter = value;
                Search();
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
        private string pages;
        public string Pages
        {
            get => pages;
            set
            {
                pages = value;
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
        public List<string> SortTypes { get; set; }
        private string selectedSortType;
        public string SelectedSortType
        {
            get => selectedSortType;
            set
            {
                selectedSortType = value;
                Sort();
            }
        }
        internal void Sort()
        {
            if (SelectedOrderType == "По умолчанию")
                return;

            if (SelectedOrderType == "По убыванию")
            {
                if (SelectedSortType == "Наименование")
                    searchResult.Sort((x, y) => y.Title.CompareTo(x.Title));
                else if (SelectedSortType == "Остаток")
                    searchResult.Sort((x, y) => ((Int32)y.CountInStock).CompareTo((Int32)x.CountInStock));
                else if (SelectedSortType == "Стоимость")
                    searchResult.Sort((x, y) => y.Cost.CompareTo(x.Cost));
            }

            if (SelectedOrderType == "По возрастанию")
            {
                if (SelectedSortType == "Наименование")
                    searchResult.Sort((x, y) => x.Title.CompareTo(y.Title));
                else if (SelectedSortType == "Остаток")
                    searchResult.Sort((x, y) => ((Int32)x.CountInStock).CompareTo((Int32)y.CountInStock));
                else if (SelectedSortType == "Стоимость")
                    searchResult.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            }
            paginationPageIndex = 0;
            Pagination();

        }
        public List<string> SearchType { get; set; }
        private string selectedSearchType;
        public string SelectedSearchType
        {
            get => selectedSearchType;
            set
            {
                selectedSearchType = value;
                Search();
            }
        }
        public List<string> OrderTypes { get; set; }
        private string selectedOrderType;
        public string SelectedOrderType
        {
            get => selectedOrderType;
            set
            {
                selectedOrderType = value;
                Sort();
            }
        }

        private ObservableCollection<Material> selectedMaterials;
        public ObservableCollection<Material> SelectedMaterials
        {
            get => selectedMaterials;
            set
            {
                selectedMaterials = value;
                SignalChanged();
            }
        }

        public CustomCommand BackPage { get; set; }
        public CustomCommand ForwardPage { get; set; }
        public CustomCommand ChangeMinCount { get; set; }
        public CustomCommand AddMaterial { get; set; }
        public CustomCommand EditMaterial { get; set; }

        private MaterialType selectedMaterialTypeFilter;
        public int rows = 0;
        public int CountPages = 0;
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

            SelectedMaterials = new ObservableCollection<Material>();

            MaterialTypeFilter = DBInstance.Get().MaterialType.ToList();
            MaterialTypeFilter.Add(new MaterialType { Title = "Все типы" });
            selectedMaterialTypeFilter = MaterialTypeFilter.Last();

            ViewCountRows = new List<string>();
            ViewCountRows.AddRange(new string[] { "15", "все" });
            selectedViewCountRows = ViewCountRows.First();

            OrderTypes = new List<string>();
            OrderTypes.AddRange(new string[] { "По умолчанию", "По убыванию", "По возрастанию" });
            selectedOrderType = OrderTypes.First();

            SearchType = new List<string>();
            SearchType.AddRange(new string[] { "Наименование", "Описание" });
            selectedSearchType = SearchType.First();

            SortTypes = new List<string>();
            SortTypes.AddRange(new string[] { "Наименование", "Остаток", "Стоимость" });
            selectedSortType = SortTypes.First();

            LoadEntities();

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
                CountPages = countPage;
                if (searchResult.Count() % rowsOnPage != 0)
                    countPage++;
                if (countPage > paginationPageIndex + 1)
                    paginationPageIndex++;
                Pagination();

            });


            AddMaterial = new CustomCommand(() =>
            {
                EditMaterial addMaterial = new EditMaterial();//here
                addMaterial.ShowDialog();//here
                LoadEntities();
                InitPagination();
                Pagination();
            });

            EditMaterial = new CustomCommand(() =>
            {
                if (SelectedMaterial == null) return;
                EditMaterial addMaterial = new EditMaterial(SelectedMaterial);//here
                addMaterial.ShowDialog();//here
                LoadEntities();
                InitPagination();
                Pagination();
            });

            searchResult = DBInstance.Get().Material.ToList();

            InitPagination();
            Pagination();

        }
        public MaterialListViewModel(List<Material> materials) : this()
        {
            EditView editView = new EditView(materials);
            editView.ShowDialog();
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

            //int.TryParse(SelectedViewCountRows, out rows);
            //CountPages = (searchResult.Count() - 1) / rows;
            //Pages = $"{paginationPageIndex + 1}/{CountPages + 1}";
        }
        private void Search()
        {
            var search = SearchText.ToLower();
            if (SelectedMaterialTypeFilter.Title == "Все типы")
            {
                if (SelectedSearchType == "Наименование")
                    searchResult = DBInstance.Get().Material
                        .Where(c => c.Title.ToLower().Contains(search)).ToList();
                else if (SelectedSearchType == "Описание")
                    searchResult = DBInstance.Get().Material
                        .Where(c => c.Description.ToLower().Contains(search)).ToList();
            }
            else
            {
                if (SelectedSearchType == "Наименование")
                    searchResult = DBInstance.Get().Material
                        .Where(c => c.Title.ToLower().Contains(search) && c.MaterialType.Title.Contains(SelectedMaterialTypeFilter.Title)).ToList();
                else if (SelectedSearchType == "Описание")
                    searchResult = DBInstance.Get().Material
                        .Where(c => c.Description.ToLower().Contains(search) && c.MaterialType.Title.Contains(SelectedMaterialTypeFilter.Title)).ToList();
            }

            Sort();
            InitPagination();
            Pagination();
        }
        public void LoadEntities()
        {
            Materials = new List<Material>(DBInstance.Get().Material.ToList());
            MaterialTypes = new List<MaterialType>(DBInstance.Get().MaterialType.ToList());
            Suppliers = new List<Supplier>(DBInstance.Get().Supplier.ToList());
            searchResult = DBInstance.Get().Material.ToList();
            foreach (var mat in Materials)
            {

                if (mat.CountInStock < mat.MinCount)
                {
                    mat.ColorForXaml = "#f19292";
                }
                else if (mat.CountInStock > mat.MinCount * 3)
                {
                    mat.ColorForXaml = "#ffba01";
                }
                mat.SupplierString = "";
                foreach (var sup in mat.Supplier)
                {
                    if (sup != mat.Supplier.Last())
                        mat.SupplierString += $"{sup.Title}, ";
                    else
                        mat.SupplierString += $"{sup.Title}";
                }
            }
        }
    }
}
