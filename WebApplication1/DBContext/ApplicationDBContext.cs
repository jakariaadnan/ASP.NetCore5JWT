using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.DBContext
{
    public class ApplicationDBContext:IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IHttpContextAccessor _httpContextAccessor) :base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<ProductMasters> productMasters { get; set; }
        public DbSet<ProductDetails> products { get; set; }
    }
}
