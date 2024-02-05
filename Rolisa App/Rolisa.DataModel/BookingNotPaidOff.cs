using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("booking_not_paid_off")]
    public partial class BookingNotPaidOff
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("booking_header_id")]
        public int? BookingHeaderId { get; set; }
        [Column("down_payment", TypeName = "decimal(18, 0)")]
        public decimal? DownPayment { get; set; }
        [Column("down_payement_date", TypeName = "datetime")]
        public DateTime? DownPayementDate { get; set; }
        [Column("payment_method_id")]
        public int? PaymentMethodId { get; set; }
        [Column("is_paid_off")]
        public bool? IsPaidOff { get; set; }
        [Column("paid_by")]
        public int? PaidBy { get; set; }
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
