using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("users")]
    public partial class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("biodata_id")]
        public int? BiodataId { get; set; }
        [Column("role_id")]
        public int? RoleId { get; set; }
        [Column("email")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Password { get; set; }
        [Column("login_attempt")]
        public int? LoginAttempt { get; set; }
        [Column("is_blocked")]
        public bool? IsBlocked { get; set; }
        [Column("unblocked_on", TypeName = "datetime")]
        public DateTime? UnblockedOn { get; set; }
        [Column("last_login", TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }
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
