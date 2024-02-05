using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("biodata")]
    public partial class Biodatum
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nickname")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Nickname { get; set; }
        [Column("fullname")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Fullname { get; set; }
        [Column("phone")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [Column("image_path")]
        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }
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
