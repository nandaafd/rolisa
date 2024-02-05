using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("token")]
    public partial class Token
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("email")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("token")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Token1 { get; set; }
        [Column("used_for")]
        [StringLength(50)]
        [Unicode(false)]
        public string? UsedFor { get; set; }
        [Column("expired_date", TypeName = "datetime")]
        public DateTime? ExpiredDate { get; set; }
        [Column("is_expired", TypeName = "datetime")]
        public DateTime? IsExpired { get; set; }
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
