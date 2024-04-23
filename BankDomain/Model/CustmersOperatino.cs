using System;
using System.Collections.Generic;

namespace BankDomain.Model;

public partial class CustmersOperatino : Entity
{
    

    public int? CustomerId { get; set; }

    public double? Sum { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Operation? Operation { get; set; }
}
