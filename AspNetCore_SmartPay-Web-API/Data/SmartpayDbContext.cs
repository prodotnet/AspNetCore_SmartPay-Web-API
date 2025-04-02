using AspNetCore_SmartPay_Web_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspNetCore_SmartPay_Web_API.Data
{
    public class SmartpayDbContext : IdentityDbContext<User>
    {

        public SmartpayDbContext(DbContextOptions<SmartpayDbContext> options) :base(options)
        {

        }


    }
}
