using System;
using System.Collections.Generic;

namespace DabaBase.Models;

public partial class City
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
