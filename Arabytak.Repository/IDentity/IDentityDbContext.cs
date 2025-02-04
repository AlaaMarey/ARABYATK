using Arabytak.Core.Entities.Identity_Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arabytak.Repository.IDentity
{
    public  class IDentityDbContext : IdentityDbContext<ApplicationUser>
    {

        public IDentityDbContext( DbContextOptions<IDentityDbContext> Opations) :base (Opations)
        { 
        }



    }
}
