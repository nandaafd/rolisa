using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolisa.ViewModel
{
    public class VMCustomer
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? CustomerFullname { get; set; }
        public string? CustomerNickname {  get; set; }
        public string? CustomerPhone {  get; set; }
        public string? AddressLabel { get; set; }
        public string? FullAddress {  get; set; }
        public string? CustomerPostalCode {  get; set; }
        public bool? AddressIsMain { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
