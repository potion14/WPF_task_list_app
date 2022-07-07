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
    /// Logika interakcji dla klasy Edycja.xaml
    /// </summary>
    public partial class Edycja : Window
    {
        public List<string> CategoriesC = new List<string> { "Szkolne", "Domowe", "Rozrywka", "Inne" };
        public List<string> UrgenciesS = new List<string> { "nieważne", "średnia ważność", "megapilne" };
        public List<StageObject> TmpStages = new List<StageObject>();

        public Edycja(string content, string category, string urgency, DateTime? startDate, DateTime? endDate, List<StageObject> stages)
        {
            InitializeComponent();
            Task.Text = content;
            CategoriesCombo.SelectedItem = category;
            UrgencyS.Value = UrgenciesS.IndexOf(urgency);
            Urgency_TextBlock.Text = UrgenciesS[Convert.ToInt32(UrgencyS.Value)];
            StartDate.SelectedDate = startDate;
            EndDate.SelectedDate = endDate;
            TmpStages = stages;
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Urgency_TextBlock.Text = UrgenciesS[Convert.ToInt32(UrgencyS.Value)];
        }

        private void ConfirmEditionOnClick(object sender, RoutedEventArgs e)
        {
            if (CategoriesCombo.Text != null && Task.Text != null && StartDate.SelectedDate != null && EndDate.SelectedDate != null &&
                TmpStages != null && StartDate.SelectedDate <= EndDate.SelectedDate) DialogResult = true;
            else MessageBox.Show("Podano nieprawidłowe wartości");
        }

        private void DeleteStage(object sender, RoutedEventArgs e)
        {
            if (TmpStages.Count != 0)
            {
                TmpStages.RemoveAt(EditWindowStagesList.SelectedIndex);
                EditWindowStagesList.Items.Refresh();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddStage(object sender, RoutedEventArgs e)
        {
            if (StagesTextBox.Text != "")
            {
                StageObject stage = new StageObject(StagesTextBox.Text);
                TmpStages.Add(stage);
                EditWindowStagesList.Items.Refresh();
                StagesTextBox.Text = "";
            }
        }

        private void Edit_window_loaded(object sender, RoutedEventArgs e)
        {
            CategoriesCombo.ItemsSource = CategoriesC;
            EditWindowStagesList.ItemsSource = TmpStages;
        }

        private void EditStage(object sender, RoutedEventArgs e)
        {
            TmpStages[EditWindowStagesList.SelectedIndex].StageItem = EditingStagesTextBox.Text;
            EditWindowStagesList.Items.Refresh();
            EditWindowStagesList.SelectedIndex = 0;
        }
    }
}
