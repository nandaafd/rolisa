using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("article")]
    public partial class Article
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tittle")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Tittle { get; set; }
        [Column("publish_date", TypeName = "datetime")]
        public DateTime? PublishDate { get; set; }
        [Column("writer")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Writer { get; set; }
        [Column("text")]
        [Unicode(false)]
        public string? Text { get; set; }
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
