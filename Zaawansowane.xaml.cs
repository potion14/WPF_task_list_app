using C1.WPF.Chart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FlexPieQuickStart
{
    /// <summary>
    /// Logika interakcji dla klasy Zaawansowane.xaml
    /// </summary>
    public partial class Zaawansowane : Window
    {
        //public List<Test.Notatka> TmpNotesList;
        //public ObservableCollection<Test.Notatka> Data;
        List<FruitDataItem> _data;
        public List<FruitDataItem> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = DataCreator.CreateFruit();
                }
                return _data;
            }
        }
        public Zaawansowane()
        {
            InitializeComponent();
            flexPie.AnimationSettings = C1.Chart.AnimationSettings.Load;
            flexPie.AnimationUpdate.Easing = C1.Chart.Easing.Linear;
            flexPie.AnimationUpdate.Duration = 500;
            flexPie.AnimationLoad.Type = C1.Chart.AnimationType.Series;
        }

    }
    class DataCreator
    {
        public static List<FruitDataItem> CreateFruit()
        {
            var fruits = new string[] { "Done", "Not done"};
            var count = fruits.Length;
            var result = new List<FruitDataItem>();
            var rnd = new Random();
            for (var i = 0; i < count; i++)
                result.Add(new FruitDataItem()
                {
                    Fruit = fruits[i],
                    March = rnd.Next(20),
                    April = rnd.Next(20),
                    May = rnd.Next(20),
                });
            return result;
        }
    }

    public class FruitDataItem
    {
        public string Fruit { get; set; }
        public double March { get; set; }
        public double April { get; set; }
        public double May { get; set; }
    }
}
