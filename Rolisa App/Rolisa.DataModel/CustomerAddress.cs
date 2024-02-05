using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("customer_address")]
    public partial class CustomerAddress
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("customer_id")]
        public int? CustomerId { get; set; }
        [Column("label")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Label { get; set; }
        [Column("full_address")]
        [StringLength(255)]
        [Unicode(false)]
        public string? FullAddress { get; set; }
        [Column("postal_code")]
        [StringLength(10)]
        [Unicode(false)]
        public string? PostalCode { get; set; }
        [Column("is_main")]
        public bool? IsMain { get; set; }
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
