using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankDomain.Model;

public partial class Bank : Entity
{
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [Display(Name = "Назва банку")]
    public string? Name { get; set; } 

    [Required(ErrorMessage = "Поле не може бути пустим")]
    [Display(Name = "Адреса банку")]
    public string? Url { get; set; }

    public virtual ICollection<Currency> Currencies { get; set; } = new List<Currency>();

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
