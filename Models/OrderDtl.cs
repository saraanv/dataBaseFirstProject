using System;
using System.Collections.Generic;

namespace DabaBase.Models;

public partial class OrderDtl
{
    public int OrderdetailId { get; set; }

    public int? ProductId { get; set; }

    public int? OrderId { get; set; }

    public double? PerPrice { get; set; }

    public double? Discount { get; set; }

    public int? Quantity { get; set; }

    public virtual OrderTbl? Order { get; set; }

    public virtual Product? Product { get; set; }
}
