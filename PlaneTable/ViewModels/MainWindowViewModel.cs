using System;
using PlaneTable.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;

namespace PlaneTable.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<TableElement> elementColection, curentColection;
        private int typeColectionFirst, typeColectionDate;

        public MainWindowViewModel()
        {
            elementColection = new ObservableCollection<TableElement>();
            curentColection = new ObservableCollection<TableElement>();
            TypeColectionFirst = 1;
            TypeColectionDate = 2;
            UpdateTableBase();
            CurentColectionUpdate(TypeColectionFirst, TypeColectionDate);
        }


        public ObservableCollection<TableElement> MainColection
        {
            get => elementColection;
            set => this.RaiseAndSetIfChanged(ref elementColection, value);
        }
        public ObservableCollection<TableElement> Colection
        {
            get => curentColection;
            set => this.RaiseAndSetIfChanged(ref curentColection, value);
        }
        public int TypeColectionFirst
        {
            get => typeColectionFirst;
            set => this.RaiseAndSetIfChanged(ref typeColectionFirst, value);
        }
        public int TypeColectionDate
        {
            get => typeColectionDate;
            set => this.RaiseAndSetIfChanged(ref typeColectionDate, value);
        }


        public void CurentColectionUpdate(int typeFirst, int typeDate)
        {
            TypeColectionFirst = typeFirst;
            TypeColectionDate = typeDate;
            string curentNaznach;
            if (typeFirst == 1) 
            { 
                curentNaznach = "Вылетел"; 
            }
            else 
            { 
                curentNaznach = "Рейс прибыл"; 
            }
            DateTime curTime = DateTime.Now;
            var needDay = 0;
            if (typeDate == 1)
            {
                needDay = curTime.Day - 1;
            }
            else if (typeDate == 2)
            {
                needDay = curTime.Day;
            }
            else
            {
                needDay = curTime.Day + 1;
            }

            curentColection.Clear();
            foreach(var element in MainColection)
            {
                if (element.Status.Equals(curentNaznach) == true && element.TimeTableTemp.Day == needDay)
                {
                    curentColection.Add(element);
                }
            }
            Colection = curentColection;
        }

        public void UpdateTableBase()
        {
            using (TableElementDataBaseContext elementContext = new TableElementDataBaseContext())
            {
                elementColection.Clear();
                var elements = elementContext.Elements.ToList();
                foreach (var element in elements)
                {
                    elementColection.Add(new TableElement
                    {
                        Id = element.Id,
                        Image = element.Image,
                        Reise = element.Reise,
                        Naznach = element.Naznach,
                        TimeTableTemp = element.TimeTableTemp,
                        TimeTableReal = element.TimeTableReal,
                        Sector = element.Sector,
                        Status = element.Status,
                        TypeTimeTable = element.TypeTimeTable,
                    });
                }
            }
            MainColection = elementColection;
            CurentColectionUpdate(TypeColectionFirst, TypeColectionDate);
        }
    }
}