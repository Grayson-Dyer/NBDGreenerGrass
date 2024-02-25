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

                // Generate 5 Clients
                if(!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            ContactFirst = "John",
                            ContactLast = "Smith",
                            Street = "999 Fake Street",
                            City = "Toronto",
                            Province = "ON",
                            Postal = "Z1Z 2Z3",
                            Phone = "111-555-5555",
                            ClientRoleID = 1
                        },
                        new Client
                        {
                            ContactFirst = "Jane",
                            ContactLast = "Doe",
                            Street = "321 Fake Street",
                            City = "Montreal",
                            Province = "QB",
                            Postal = "A3B 3C3",
                            Phone = "999-555-5555",
                            ClientRoleID = 2
                        },
                        new Client
                        {
                            ContactFirst = "Billy",
                            ContactLast = "Joel",
                            Street = "111 Real Street",
                            City = "Toronto",
                            Province = "ON",
                            Postal = "A3A 3A3",
                            Phone = "222-555-5555",
                            ClientRoleID = 2
                        },
                        new Client
                        {
                            ContactFirst = "Real",
                            ContactLast = "Person",
                            Street = "321 Electric Avenue",
                            City = "St. Catharines",
                            Province = "ON",
                            Postal = "A1A 1A1",
                            Phone = "333-555-5555",
                            ClientRoleID = 1
                        },
                        new Client
                        {
                            ContactFirst = "Bob",
                            ContactLast = "Ross",
                            Street = "123 Fake Street",
                            City = "Kingston",
                            Province = "ON",
                            Postal = "A1B 2C3",
                            Phone = "416-555-5555",
                            ClientRoleID = 1
                        });

                        context.SaveChanges();
                }
                // Generate 5 Projects
                if(!context.Projects.Any())
                {
                    context.Projects.AddRange(
                        new Project
                        {
                            Start = DateTime.Now,
                            End = DateTime.Now.AddDays(7),
                            City = "Toronto",
                            Province = "ON",
                            Postal = "Z1Z 1Z1",
                            Street = "123 Fake Street",
                            Created = DateTime.Now,
                            Amount = 1000,
                            ClientID = context.Clients.FirstOrDefault(c => c.ContactFirst == "John").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now,
                            End = DateTime.Now.AddDays(7),
                            City = "Montreal",
                            Province = "QB",
                            Postal = "A1A 1A1",
                            Street = "456 Fake Street",
                            Created = DateTime.Now,
                            Amount = 2000,
                            ClientID = context.Clients.FirstOrDefault(c => c.ContactFirst == "Jane").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now,
                            End = DateTime.Now.AddDays(7),
                            City = "Toronto",
                            Province = "ON",
                            Postal = "B1B 1B1",
                            Street = "23 Real Street",
                            Created = DateTime.Now,
                            Amount = 2000,
                            ClientID = context.Clients.FirstOrDefault(c => c.ContactFirst == "Billy").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now,
                            End = DateTime.Now.AddDays(7),
                            City = "St. Catharines",
                            Province = "ON",
                            Postal = "C1C 1C1",
                            Street = "99 Real Street",
                            Created = DateTime.Now,
                            Amount = 2000,
                            ClientID = context.Clients.FirstOrDefault(c => c.ContactFirst == "Real").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now,
                            End = DateTime.Now.AddDays(7),
                            Province = "ON",
                            Postal = "Z0Z 0Z0",
                            Street = "789 Fake Street",
                            City = "Toronto",
                            Created = DateTime.Now,
                            Amount = 3000,
                            ClientID = context.Clients.FirstOrDefault(c => c.ContactFirst == "Bob").ID
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
