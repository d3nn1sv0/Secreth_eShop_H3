using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProductCreateViewModel
{
    public Product Product { get; set; } = new Product();
    public int SelectedSupplierId { get; set; }
    public int SelectedCategoryId { get; set; }
}


