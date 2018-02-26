using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }

        public DbSet<UserDataTest> UserData { get; set; }
    }
}
