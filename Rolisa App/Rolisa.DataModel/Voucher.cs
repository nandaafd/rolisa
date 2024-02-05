using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("voucher")]
    public partial class Voucher
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("start_date", TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column("end_date", TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("code")]
        [StringLength(15)]
        [Unicode(false)]
        public string? Code { get; set; }
        [Column("discount_presentage", TypeName = "decimal(18, 0)")]
        public decimal? DiscountPresentage { get; set; }
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
