using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankDomain.Model;

public partial class Currency : Entity
{

    [Required(ErrorMessage = "Поле не може бути пустим")]
    [Display(Name = "Назва Валюти")]
    public string? CurrencyName { get; set; }
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [Display(Name = "Банк")]
    public int? BankId { get; set; }
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [Display(Name = "Курс")]
    public double? CurrencyRate { get; set; }
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [Display(Name = "Банк що торгує")]
    public virtual Bank? Bank { get; set; } 
}
