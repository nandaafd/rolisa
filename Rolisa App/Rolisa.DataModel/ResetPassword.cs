using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("reset_password")]
    public partial class ResetPassword
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("old_password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? OldPassword { get; set; }
        [Column("new_password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? NewPassword { get; set; }
        [Column("used_for")]
        [StringLength(255)]
        [Unicode(false)]
        public string? UsedFor { get; set; }
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
