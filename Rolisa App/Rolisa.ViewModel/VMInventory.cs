﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Rolisa.ViewModel
{
    public class VMInventory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? InventoryCategoryId { get; set; }
        public string? InventoryCategoryName { get; set; }
        public int? ConditionId { get; set; }
        public string? ConditionName { get; set; }
        public int? Qty { get; set; }
        public IFormFile? image { get; set; }
        public string? ImagePath { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
