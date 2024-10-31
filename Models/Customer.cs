using System;
using System.Collections.Generic;

namespace DabaBase.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerFName { get; set; }

    public string? CustomerLName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Postalcode { get; set; }

    public string? Password { get; set; }

    public int? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<OrderTbl> OrderTbls { get; set; } = new List<OrderTbl>();
}
