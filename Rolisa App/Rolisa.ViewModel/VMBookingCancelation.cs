using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolisa.ViewModel
{
    public class VMBookingCancelation
    {
        public int Id { get; set; }
        public int? OrderHeaderId { get; set; }
        public string? Description { get; set; }
        public decimal? AmountMoneyReturned { get; set; }
        public int? PaymentMethodId { get; set; }
        public string? UserAccountName { get; set; }
        public string? UserAccountNumber { get; set; }
        public string? ReturnedProof { get; set; }
        public bool? IsReturned { get; set; }
        public int? CanceledBy { get; set; }
        public int? CancelationStatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
