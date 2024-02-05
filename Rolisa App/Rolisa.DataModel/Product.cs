using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("product")]
    public partial class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("description")]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("stock_qty")]
        public int? StockQty { get; set; }
        [Column("price", TypeName = "decimal(18, 0)")]
        public decimal? Price { get; set; }
        [Column("product_category_id")]
        public int? ProductCategoryId { get; set; }
        [Column("image_1")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Image1 { get; set; }
        [Column("image_2")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Image2 { get; set; }
        [Column("image_3")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Image3 { get; set; }
        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("modified_by")]
        public int? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public int? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_deleted")]
        public bool? IsDeleted { get; set; }
    }
}
