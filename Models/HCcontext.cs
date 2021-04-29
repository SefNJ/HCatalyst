using Microsoft.EntityFrameworkCore;

namespace HCatalyst.Models
{
    public class HCcontext : DbContext
    {
        public HCcontext(DbContextOptions<HCcontext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The list of HCperson that is stored for the length of the application.
        /// </summary>
        public DbSet<HCperson> HCpersons { get; set; }
    }
}