using Microsoft.EntityFrameworkCore;
using VcardQRGenerator.Models;

namespace VcardQRGenerator.Data
{
    public class VcardDbContext : DbContext
    {

        public VcardDbContext(DbContextOptions<VcardDbContext> options) : base(options)
        {
        }

        public DbSet<Vcard> Vcards { get; set; }




    }
}
