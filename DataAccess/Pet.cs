using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Pet
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public DateOnly? SoldDate { get; set; }

    public decimal Price { get; set; }

    public string Seller { get; set; } = null!;

    public virtual Seller SellerNavigation { get; set; } = null!;
}
