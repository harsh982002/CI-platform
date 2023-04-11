using CIPlatform.Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class TimeSheetViewModel
    {
        public List<CIPlatform.Entitites.ViewModel.MissionSelectViewModel>? mission { get; set; }

        public List<Timesheet>? timesheets { get; set; }

        public int? Hours { get; set; }

        public long? mission_id { get; set; }

        public int? minutes { get; set; }

        public string? message { get; set; }

        public int? action { get; set; }

        public string? date { get; set; }

    }
}
