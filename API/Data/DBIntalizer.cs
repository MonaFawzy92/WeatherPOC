using Domain.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class DBIntalizer
    {
        public static void seed(IApplicationBuilder app)
        {
            // seed Data
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(
                        new Employee { Id = Guid.NewGuid(), FirstName = "Mohamed", LastName = "Abdelaty", MobileNumber = "01201216729", CreatedBy = "admin", ModifiedBy = null, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now.AddDays(1) },
                        new Employee { Id = Guid.NewGuid(), FirstName = "Ahmed", LastName = "Mohamed", MobileNumber = "0105432190", CreatedBy = "admin", ModifiedBy = null, CreatedOn = DateTime.Now.AddDays(-1), ModifiedOn = DateTime.Now.AddDays(1) }
                    );
                    context.SaveChanges();
                }
            }
        }

    }
}
