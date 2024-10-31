using System;
using System.Collections.Generic;

namespace DabaBase.Models;

public partial class OrderTbl
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? IsDelivered { get; set; }

    public int? CustomerId { get; set; }

    public double? Discount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDtl> OrderDtls { get; set; } = new List<OrderDtl>();
}
