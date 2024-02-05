using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("payment_method")]
    public partial class PaymentMethod
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("account_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? AccountName { get; set; }
        [Column("account_number")]
        [StringLength(255)]
        [Unicode(false)]
        public string? AccountNumber { get; set; }
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
