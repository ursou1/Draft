using Draft.DB;
using Draft.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draft.ViewModels
{
    public class EditViewModel: BaseViewModel
    {
        private string minCountValue;
        public string MinCountValue
        {
            get => minCountValue;
            set
            {
                minCountValue = value;
                SignalChanged();
            }
        }
        public List<Material> Materials;

        public CustomCommand Save { get; set; }

        public EditViewModel(List<Material> materials)
        {
            var connection = DBInstance.Get();
            materials.Sort((x, y) => x.MinCount.CompareTo(y.MinCount));
            MinCountValue = materials.Last().MinCount.ToString();
            SignalChanged("MinCountValue");


            Save = new CustomCommand(() =>
            {
                foreach (var mat in materials)
                {
                    mat.MinCount = int.Parse(MinCountValue);
                }
                connection.SaveChanges();

            });
        }
    }
}
