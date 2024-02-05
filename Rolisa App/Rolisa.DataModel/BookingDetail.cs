using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("booking_detail")]
    public partial class BookingDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("booking_header_id")]
        public int? BookingHeaderId { get; set; }
        [Column("product_id")]
        public int? ProductId { get; set; }
        [Column("qty")]
        public int? Qty { get; set; }
        [Column("price", TypeName = "decimal(18, 0)")]
        public decimal? Price { get; set; }
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
