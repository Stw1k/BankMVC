using System;
using System.Collections.Generic;

namespace BankDomain.Model;

public partial class CustomerAccoount : Entity
{
    

    public int? Number { get; set; }

    public string? Currency { get; set; }

    public int? BankId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? StartDate { get; set; }

    public virtual Customer? Customer { get; set; }
}
