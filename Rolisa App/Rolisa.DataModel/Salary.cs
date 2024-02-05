using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("salary")]
    public partial class Salary
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("employee_id")]
        public int? EmployeeId { get; set; }
        [Column("booking_header_id")]
        public int? BookingHeaderId { get; set; }
        [Column("salary", TypeName = "decimal(18, 0)")]
        public decimal? Salary1 { get; set; }
        [Column("payment_date", TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }
        [Column("paid_off_date", TypeName = "datetime")]
        public DateTime? PaidOffDate { get; set; }
        [Column("is_paid_off")]
        public bool? IsPaidOff { get; set; }
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
