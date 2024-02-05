﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    [Table("rating")]
    public partial class Rating
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("customer_id")]
        public int? CustomerId { get; set; }
        [Column("product_id")]
        public int? ProductId { get; set; }
        [Column("stars", TypeName = "decimal(18, 0)")]
        public decimal? Stars { get; set; }
        [Column("comment")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Comment { get; set; }
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