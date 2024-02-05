using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolisa.ViewModel
{
    public class VMSalaryNotPaidOff
    {
        public int Id { get; set; }
        public int? SalaryId { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? RemainingAmount { get; set; }
        public string? Description { get; set; }
        public bool? IsPaid { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
