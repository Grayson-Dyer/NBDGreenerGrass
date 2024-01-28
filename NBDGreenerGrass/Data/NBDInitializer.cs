using NBDGreenerGrass.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace NBDGreenerGrass.Data
{
    public class NBDInitializer
    {

        public static void Seed(IApplicationBuilder application)
        {
            NBDContext context = application.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<NBDContext>();

            try
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Random random = new Random();

                // Generate 3 Clients
                if(!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            FirstName = "John",
                            LastName = "Smith",
                            Address = "123 Fake Street",
                            Phone = "416-123-4567",
                            ContactRole = "Manager"
                        },
                        new Client
                        {
                            FirstName = "Jane",
                            LastName = "Doe",
                            Address = "456 Fake Street",
                            Phone = "416-987-6543",
                            ContactRole = "Manager"
                        },
                        new Client
                        {
                            FirstName = "Bob",
                            LastName = "Ross",
                            Address = "789 Fake Street",
                            Phone = "416-555-5555",
                            ContactRole = "Manager"
                        });

                        context.SaveChanges();
                }
                // Generate 3 Projects
                if(!context.Projects.Any())
                {
                    context.Projects.AddRange(
                        new Project
                        {
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            Location = "123 Fake Street",
                            DateMade = DateTime.Now,
                            Amount = 1000,
                            ClientID = context.Clients.FirstOrDefault(c => c.FirstName == "John").ID
                        },
                        new Project
                        {
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            Location = "456 Fake Street",
                            DateMade = DateTime.Now,
                            Amount = 2000,
                            ClientID = context.Clients.FirstOrDefault(c => c.FirstName == "Jane").ID
                        },
                        new Project
                        {
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            Location = "789 Fake Street",
                            DateMade = DateTime.Now,
                            Amount = 3000,
                            ClientID = context.Clients.FirstOrDefault(c => c.FirstName == "Bob").ID
                        }) ;
                        context.SaveChanges();
                }
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
