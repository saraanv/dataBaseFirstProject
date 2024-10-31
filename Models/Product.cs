using System;
using System.Collections.Generic;

namespace DabaBase.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public double? ProductPrice { get; set; }

    public int? ProductStock { get; set; }

    public DateTime? PchangeDate { get; set; }

    public double? Poldprice { get; set; }

    public double? Pnewprice { get; set; }

    public int? Prate { get; set; }

    public string? ProductDesc { get; set; }

    public int? BrandId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<OrderDtl> OrderDtls { get; set; } = new List<OrderDtl>();
}
