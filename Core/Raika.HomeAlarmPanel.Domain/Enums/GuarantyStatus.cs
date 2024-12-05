using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Domain.Enums
{
    public enum GuarantyStatus
    {
        [Display(Name = "Active")]
        NoteList = 1,
        [Display(Name = "Inactive")]
        Nottitle = 2,
        [Display(Name = "None")]
        NotContent = 3,
    }
}
