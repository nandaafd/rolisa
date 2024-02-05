using Microsoft.EntityFrameworkCore;

namespace Rolisa.DataModel
{
    public class RolisaContext : DbContext
    {
        public RolisaContext() { }
        public RolisaContext(DbContextOptions<RolisaContext> options) : base(options) { }   
    }
}
