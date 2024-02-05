using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("booking_cancelation")]
    public partial class BookingCancelation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("order_header_id")]
        public int? OrderHeaderId { get; set; }
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("amount_money_returned", TypeName = "decimal(18, 0)")]
        public decimal? AmountMoneyReturned { get; set; }
        [Column("payment_method_id")]
        public int? PaymentMethodId { get; set; }
        [Column("user_account_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? UserAccountName { get; set; }
        [Column("user_account_number")]
        [StringLength(255)]
        [Unicode(false)]
        public string? UserAccountNumber { get; set; }
        [Column("returned_proof")]
        [StringLength(255)]
        [Unicode(false)]
        public string? ReturnedProof { get; set; }
        [Column("is_returned")]
        public bool? IsReturned { get; set; }
        [Column("canceled_by")]
        public int? CanceledBy { get; set; }
        [Column("cancelation_status_id")]
        public int? CancelationStatusId { get; set; }
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
