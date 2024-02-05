using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("booking_header")]
    public partial class BookingHeader
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("trx_code")]
        [StringLength(255)]
        [Unicode(false)]
        public string? TrxCode { get; set; }
        [Column("customer_id")]
        public int? CustomerId { get; set; }
        [Column("customer_address_id")]
        public int? CustomerAddressId { get; set; }
        [Column("customer_contact")]
        [StringLength(255)]
        [Unicode(false)]
        public string? CustomerContact { get; set; }
        [Column("client_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? ClientName { get; set; }
        [Column("start_from", TypeName = "datetime")]
        public DateTime? StartFrom { get; set; }
        [Column("end_at", TypeName = "datetime")]
        public DateTime? EndAt { get; set; }
        [Column("amount", TypeName = "decimal(18, 0)")]
        public decimal? Amount { get; set; }
        [Column("total_qty")]
        public int? TotalQty { get; set; }
        [Column("voucher_id")]
        public int? VoucherId { get; set; }
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
