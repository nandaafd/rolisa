using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolisa.ViewModel
{
    public class VMRegister
    {
        public VMBiodatum? Biodatum { get; set; }
        public VMUser? User { get; set; }
        public VMCustomer? Customer { get; set; }
    }
}
