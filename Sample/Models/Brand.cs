using System;
using System.Collections.Generic;

namespace Sample.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Items { get; set; } = new List<Product>();
}
