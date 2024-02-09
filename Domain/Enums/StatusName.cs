using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum StatusName
    {
        [Display(Name = "Добавлена")]
        Added = 0,
        [Display(Name = "В работе")]
        InProgress = 1,
        [Display(Name = "Завершена")]
        Complete = 2
    }
}
