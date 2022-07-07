using System;
using System.Collections;
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
using System.Diagnostics;

namespace Test
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Notatka> Notes = new ObservableCollection<Notatka>();
        public MainWindow()
        {
            InitializeComponent();
            Notes.Add(new Notatka("Szkolne", "Zrobić projekt z WPF", "megapilne", new DateTime(2009, 3, 5), new DateTime(2009, 12, 5),
                new List<StageObject> { new StageObject("Wybrać temat", true), new StageObject("Zaprojektować Xamla"),
                new StageObject("Obsłużyć kontrolki"), new StageObject("Dodać style" ) }, true));
            Notes.Add(new Notatka("Rozrywka", "Kupić bilety do kina", "nieważne", new DateTime(2010, 4, 13), new DateTime(2010, 4, 14),
                new List<StageObject> { new StageObject("Pójść do kina"), new StageObject("Kupić bilet") }));
            Notes.Add(new Notatka("Rozrywka", "Zagrać w grę", "megapilne", new DateTime(2009, 1, 1), new DateTime(2009, 12, 29),
                new List<StageObject> { new StageObject("Uruchomic komputer"), new StageObject("Odpalić grę"), new StageObject("Zebrać zespół") }));
            Notes.Add(new Notatka("Inne", "Zawiesić lampki na balkonie", "nieważne", new DateTime(2011, 9, 5), new DateTime(2011, 12, 5),
                new List<StageObject> { new StageObject("Kupić lampki"), new StageObject("Przeczytać instrukcję"), new StageObject("Zawiesić lampki") }));
            Notes.Add(new Notatka("Inne", "Kupić prezent siostrze", "średnia ważność", new DateTime(2009, 3, 5), new DateTime(2009, 12, 15),
                new List<StageObject> { new StageObject("Wymyślić co kupić"), new StageObject("Znaleźć w internecie"), new StageObject("Zamówić") }));
            Notes.Add(new Notatka("Domowe", "Posprzątać pokój", "średnia ważność", new DateTime(2009, 7, 3), new DateTime(2009, 8, 30),
                new List<StageObject> { new StageObject("Odkurzanie"), new StageObject("Poskładać ubrania"), new StageObject("Wytrzeć kurze"),
                new StageObject("Pozbierać śmieci"), new StageObject("Zasłać łóżko"), new StageObject("Posegregować książki") }));
            Notes.Add(new Notatka("Rozrywka", "Spotkać z kolegą", "megapilne", new DateTime(2010, 3, 5), new DateTime(2010, 12, 5),
                new List<StageObject> { new StageObject("Ustalić czas spotkania"), new StageObject("Wybrać miejsce spotkania") }));
            Notes.Add(new Notatka("Inne", "Przygotować rower do sezonu", "średnia ważność", new DateTime(2011, 12, 5), new DateTime(2011, 12, 31),
                new List<StageObject> { new StageObject("Kupić łańcuch"), new StageObject("Wymienić siodełko"), new StageObject("Wymienić dętkę"),
                    new StageObject("Wyregulować przerzutki"), new StageObject("Wyregulować hamulce") }));
            Notes.Add(new Notatka("Rozrywka", "Pójść na basen", "średnia ważność", new DateTime(2009, 3, 5), new DateTime(2009, 12, 5),
                new List<StageObject> { new StageObject("Kupić karnet"), new StageObject("Sprawdzić godziny otwarcia") }));
            Notes.Add(new Notatka("Domowe", "Zrobić zakupy", "megapilne", new DateTime(2009, 3, 5), new DateTime(2009, 12, 5),
                new List<StageObject> { new StageObject("Sporządzić listę zakupów"), new StageObject("Pojechać do sklepu") }));
            Notes.Add(new Notatka("Rozrywka", "Obejrzenie filmu", "nieważne", new DateTime(2009, 3, 5), new DateTime(2009, 12, 5),
                new List<StageObject> { new StageObject("Wybrać film"), new StageObject("Zanleźć film na serwisie streamingowym") }));
            Notes.Add(new Notatka("Szkolne", "Kolokwium z SBD", "megapilne", new DateTime(2009, 3, 5), new DateTime(2009, 12, 5),
                new List<StageObject> { new StageObject("Przejrzeć notatki"), new StageObject("Poćwiczyć zadania"), new StageObject("Napisać kolosa") }));
            MainList.ItemsSource = Notes;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MainList.ItemsSource);
            view.Filter = UserFilter;
        }

        private void Zamknij(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Dodaj_zadanie(object sender, RoutedEventArgs e)
        {
            Dodawanie add = new Dodawanie();
            add.ShowDialog();
            if(add.DialogResult == true)
            {
                add.Add();
                Notes.Add(add.nowaNotatka);
                MainList.Items.Refresh();
                
            }
        }
        private void Usun_zadanie(object sender, RoutedEventArgs e)
        {
            if (MainList.SelectedItem != null)
            {
                MessageBoxResult wybor = MessageBox.Show("Czy na pewno chcesz usunąć?", "Ostrzeżenie", MessageBoxButton.YesNo);
                switch (wybor)
                {
                    case MessageBoxResult.Yes:
                        StagesList.ItemsSource = Notes.ElementAt(0).Stages;
                        Notes.RemoveAt(MainList.SelectedIndex);
                        MainList.SelectedIndex = 0;
                        //MainList.Items.Refresh();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }
        private void Edytuj_zadanie(object sender, RoutedEventArgs e)
        {
            Edycja edycja = new Edycja
                ( 
                    Notes.ElementAt(MainList.SelectedIndex).Content,
                    Notes.ElementAt(MainList.SelectedIndex).Category,
                    Notes.ElementAt(MainList.SelectedIndex).Urgency,
                    Notes.ElementAt(MainList.SelectedIndex).StartDate,
                    Notes.ElementAt(MainList.SelectedIndex).EndDate,
                    Notes.ElementAt(MainList.SelectedIndex).Stages
                );
            edycja.ShowDialog();
            if(edycja.DialogResult == true)
            {
                Notes.ElementAt(MainList.SelectedIndex).Content = edycja.Task.Text;
                Notes.ElementAt(MainList.SelectedIndex).Category = Convert.ToString(edycja.CategoriesCombo.SelectedValue);
                Notes.ElementAt(MainList.SelectedIndex).Urgency = edycja.UrgenciesS[Convert.ToInt32(edycja.UrgencyS.Value)];
                Notes.ElementAt(MainList.SelectedIndex).StartDate = edycja.StartDate.SelectedDate;
                Notes.ElementAt(MainList.SelectedIndex).EndDate = edycja.EndDate.SelectedDate;
                Notes.ElementAt(MainList.SelectedIndex).Stages = edycja.TmpStages;
            }
            MainList.Items.Refresh();
            StagesList.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private ListCollectionView View
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(Notes); }
        }

        private void SortNone(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.CustomSort = null;
        }
        private void SortName(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Content", System.ComponentModel.ListSortDirection.Ascending));
        }
        private void SortCategory(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Category", System.ComponentModel.ListSortDirection.Ascending));
        }
        private void SortDate(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new System.ComponentModel.SortDescription("EndDate", System.ComponentModel.ListSortDirection.Ascending));
        }
        private void GroupNone(object sender, RoutedEventArgs e)
        {
            View.GroupDescriptions.Clear();
        }
        private void GroupUrgency(object sender, RoutedEventArgs e)
        {
            View.GroupDescriptions.Clear();
            View.GroupDescriptions.Add(new PropertyGroupDescription("Urgency"));
        }
        private void GroupCategory(object sender, RoutedEventArgs e)
        {
            View.GroupDescriptions.Clear();
            View.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        }

        private void DeleteStage(object sender, RoutedEventArgs e)
        {
            Notes.ElementAt(MainList.SelectedIndex).Stages.RemoveAt(StagesList.SelectedIndex);
            StagesList.Items.Refresh();
        }

        private void Niespodzianka(object sender, RoutedEventArgs e)
        {
            Process.Start("C:/Users/Piotr/source/repos/Test/Tetris2.exe");
        }

        private void TaskCheckboxChecked(object sender, RoutedEventArgs e)
        {
            var item = (CheckBox)sender;
            try
            {
                bool tmp = false;
                if (item.IsChecked == true) tmp = true;
                for (int i = 0; i < Notes.ElementAt(MainList.SelectedIndex).Stages.Count; i++)
                {
                    if (Notes.ElementAt(MainList.SelectedIndex).Stages.ElementAt(i).ToString().Equals(item.Tag.ToString()))
                        Notes.ElementAt(MainList.SelectedIndex).Stages.ElementAt(i).IsStagesListCheckBoxChecked = tmp;
                }
            }
            catch { }
        }

        private void MainList_selection(object sender, RoutedEventArgs e)
        {
            try
            {
                StagesList.ItemsSource = Notes.ElementAt(MainList.SelectedIndex).Stages;
            }
            catch { }
        }

        private void StagesCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            var item = (CheckBox)sender;
            try
            {
                bool tmp = false;
                if (item.IsChecked == true) tmp = true;
                for(int i = 0; i < Notes.ElementAt(MainList.SelectedIndex).Stages.Count; i++)
                {
                    if (Notes.ElementAt(MainList.SelectedIndex).Stages.ElementAt(i).ToString().Equals(item.Tag.ToString()))
                    Notes.ElementAt(MainList.SelectedIndex).Stages.ElementAt(i).IsStagesListCheckBoxChecked = tmp;
                }
            }
            catch { }
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTextBox.Text))
                return true;
            else
                return ((item as Notatka).Content.IndexOf(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void Searching(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(MainList.ItemsSource).Refresh();
        }

        private void AdwancedOnClick(object sender, RoutedEventArgs e)
        {
            FlexPieQuickStart.Zaawansowane z = new FlexPieQuickStart.Zaawansowane();
            //z.Data = Notes;
            z.ShowDialog();
        }
    }
}
