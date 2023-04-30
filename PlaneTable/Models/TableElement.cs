using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneTable.Models
{
    public class TableElement 
    {
        public int Id { get; set; }

        public string? Reise { get; set; }
        public string? Image { get; set; }
        public int TypeTimeTable { get; set; }
        public string? Naznach { get; set; }
        public DateTime TimeTableTemp { get; set; }
        public DateTime TimeTableReal { get; set; }
        public string StringTimeTableTemp { get => TimeTableTemp.ToShortTimeString(); }
        public string StringTimeTableReal { get => TimeTableReal.ToShortTimeString(); }
        public string? Status { get; set; }
        public string? Sector { get; set; }
        
    }
}
