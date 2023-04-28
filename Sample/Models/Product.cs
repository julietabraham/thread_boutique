using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
namespace Sample.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public partial class Product
{
    public int ProductId { get; set; }
    [Required(ErrorMessage= "Category required")]
    
    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    [Required(ErrorMessage = "Title required")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Price required")]
    [Range(1,1500,ErrorMessage ="Price must be between $1 and $1500")]
    public decimal? Price { get; set; }
    [Required(ErrorMessage = "Image required")]
    public string? ItemUrl { get; set; }

    public virtual Category Category { get; set; }

    public virtual Brand Brand { get; set; } = null!;
}
