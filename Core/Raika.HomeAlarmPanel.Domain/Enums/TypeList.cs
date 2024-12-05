using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Domain.Enums
{
    public enum TypeList
    {
        [Display(Name = "NoteList")]
        NoteList = 1,
        [Display(Name = "Nottitle")]
        Nottitle = 2,
        [Display(Name = "NotContent")]
        NotContent = 3,
    }
}
