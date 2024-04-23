using System;
using System.Collections.Generic;

namespace BankDomain.Model;

public partial class Customer : Entity
{
    

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Adress { get; set; }

    public string? PhoneNumber { get; set; }

    public int? BankAccountId { get; set; }

    public int? OperationId { get; set; }

    public virtual ICollection<CustmersOperatino> CustmersOperatinos { get; set; } = new List<CustmersOperatino>();

    public virtual ICollection<CustomerAccoount> CustomerAccoounts { get; set; } = new List<CustomerAccoount>();
}
