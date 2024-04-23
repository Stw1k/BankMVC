using System;
using System.Collections.Generic;

namespace BankDomain.Model;

public partial class Operation : Entity
{
    

    public int? BankId { get; set; }

    public string? NameOperation { get; set; }

    public double? ServicePercant { get; set; }

    public virtual Bank? Bank { get; set; }

    public virtual CustmersOperatino IdNavigation { get; set; } = null!;
}
