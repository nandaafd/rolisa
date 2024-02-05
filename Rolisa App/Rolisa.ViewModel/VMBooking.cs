using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolisa.ViewModel
{
    public class VMBooking
    {
        public int Id { get; set; }
        public string? TrxCode { get; set; }
        public int? CustomerId { get; set; }
        public int? CustomerAddressId { get; set; }
        public string? CustomerContact { get; set; }
        public string? ClientName { get; set; }
        public DateTime? StartFrom { get; set; }
        public DateTime? EndAt { get; set; }
        public decimal? Amount { get; set; }
        public int? TotalQty { get; set; }
        public int? VoucherId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<VMBookingDetail> Details { get; set; }
    }
}
