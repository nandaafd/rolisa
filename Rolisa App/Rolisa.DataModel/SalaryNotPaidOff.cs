using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("salary_not_paid_off")]
    public partial class SalaryNotPaidOff
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("salary_id")]
        public int? SalaryId { get; set; }
        [Column("amount_paid", TypeName = "decimal(18, 0)")]
        public decimal? AmountPaid { get; set; }
        [Column("remaining_amount", TypeName = "decimal(18, 0)")]
        public decimal? RemainingAmount { get; set; }
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("is_paid")]
        public bool? IsPaid { get; set; }
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
