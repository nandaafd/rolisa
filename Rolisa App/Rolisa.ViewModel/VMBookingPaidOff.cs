using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolisa.ViewModel
{
    public class VMBookingPaidOff
    {
        public int Id { get; set; }
        public int? BookingHeaderId { get; set; }
        public decimal? AmountPaid { get; set; }
        public DateTime? PaidDate { get; set; }
        public int? PaymentMethodId { get; set; }
        public bool? IsPaidOff { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
