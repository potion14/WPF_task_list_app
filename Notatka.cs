using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Notatka : INotifyPropertyChanged
    {
        public string Content { get; set; }  //Zawartość notatki
        public string Category { get; set; }  //Kategoria
        public string Urgency { get; set; }  //Pilnosc
        public DateTime? StartDate { get; set; }  //Data rozpoczęcia
        public DateTime? EndDate { get; set; }  //Data zakonczenia
        public string ImageTitle { get; set; }  //Tytuł obrazka do kategorii
        public List<StageObject> Stages { get; set; }  //Etapy
        //public StagesL Stages { get; set; }
        public bool IsTaskChecked { get; set; }  //Czy zadanie jest zaznaczone jako ukończone

        public Notatka(string category, string content, string urgency, DateTime? startdate, DateTime? enddate, List<StageObject> stages, bool isTaskChecked = false)
        {
            Category = category;
            Content = content;
            Urgency = urgency;
            StartDate = startdate;
            EndDate = enddate;
            ImageTitle = category + ".png";
            Stages = stages;
            IsTaskChecked = isTaskChecked;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                new PropertyChangedEventArgs(property));
        }
    }

    public class StageObject : INotifyPropertyChanged
    {
        public string StageItem { get; set; }  //Etapy
        public bool IsStagesListCheckBoxChecked { get; set; }  //Czy jest zaznaczone

        public StageObject(string stageItem = null, bool checkbox = false)
        {
            StageItem = stageItem;
            IsStagesListCheckBoxChecked = checkbox;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                new PropertyChangedEventArgs(property));
        }

        public override string ToString()
        {
            return StageItem;
        }
    }

}
