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

namespace Test
{
    /// <summary>
    /// Logika interakcji dla klasy Dodawanie.xaml
    /// </summary>
    public partial class Dodawanie : Window
    {
        public List<string> CategoriesC = new List<string> { "Szkolne", "Domowe", "Rozrywka", "Inne" };
        public List<string> UrgenciesS = new List<string> { "nieważne", "średnia ważność", "megapilne" };
        public List<StageObject> TmpStages = new List<StageObject>();
        public Notatka nowaNotatka = new Notatka("", "", "", null, null, null);

        public Dodawanie()
        {
            InitializeComponent();
        }
        public void Add()
        {
            nowaNotatka.Category = CategoriesCombo.Text;
            nowaNotatka.Content = Task.Text;
            nowaNotatka.Urgency = UrgenciesS[Convert.ToInt32(UrgencyS.Value)];
            nowaNotatka.StartDate = StartDate.SelectedDate;
            nowaNotatka.EndDate = EndDate.SelectedDate;
            nowaNotatka.ImageTitle = "C:/Users/Piotr/source/repos/Test/Images/" + nowaNotatka.Category + ".png";
            nowaNotatka.Stages = TmpStages;
        }

        private void Add_Window_Loaded(object sender, RoutedEventArgs e)
        {
            CategoriesCombo.ItemsSource = CategoriesC;
            AddWindowStagesList.ItemsSource = TmpStages;
            Urgency_TextBlock.Text = UrgenciesS[0];
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Urgency_TextBlock.Text = UrgenciesS[Convert.ToInt32(UrgencyS.Value)];
        }

        private void AddNoteOnClick(object sender, RoutedEventArgs e)
        {
            if (CategoriesCombo.Text != null && Task.Text != null && StartDate.SelectedDate != null && EndDate.SelectedDate != null &&
                TmpStages != null && StartDate.SelectedDate <= EndDate.SelectedDate) DialogResult = true;
            else MessageBox.Show("Podano nieprawidłowe wartości");
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AddStage(object sender, RoutedEventArgs e)
        {
            if(StagesTextBox.Text != "")
            {
                StageObject stage = new StageObject(StagesTextBox.Text);
                TmpStages.Add(stage);
                AddWindowStagesList.Items.Refresh();
                StagesTextBox.Text = "";
            }
        }

        private void DeleteStage(object sender, RoutedEventArgs e)
        {
            if(TmpStages.Count != 0)
            {
                TmpStages.RemoveAt(AddWindowStagesList.SelectedIndex);
                AddWindowStagesList.Items.Refresh();
            }
        }
    }
}
