using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Rolisa.ViewModel
{
    public class VMProduct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? StockQty { get; set; }
        public decimal? Price { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? Image1 { get; set; }
        public IFormFile? Image1File { get; set; }
        public string? Image2 { get; set; }
        public IFormFile? Image2File { get; set; }
        public string? Image3 { get; set; }
        public IFormFile? Image3File { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
