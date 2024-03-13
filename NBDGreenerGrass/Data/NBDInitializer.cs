using NBDGreenerGrass.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using NBDGreenerGrass.Enums;
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
                context.Database.Migrate();
                context.Database.EnsureCreated();

                Random random = new Random();
                if (!context.Inventories.Any())
                {
                    context.AddRange(
                        new Inventory
                        {
                            InventoryDesc = "Decorative Cedar bark",
                            InventorySize = "5 cu ft",
                            InventoryCode = "CBRK5",
                            InventoryListPrice = 15.95m
                        },
                        new Inventory
                        {
                            InventoryDesc = "Crushed Grantie",
                            InventorySize = "1 Yard",
                            InventoryCode = "CRGRN",
                            InventoryListPrice = 14.00m
                        },
                        new Inventory
                        {
                            InventoryDesc = "Pea Gravel",
                            InventorySize = "1 Yard",
                            InventoryCode = "PGRV",
                            InventoryListPrice = 20.00m
                        },
                        new Inventory
                        {
                            InventoryDesc = "1\" Gravel",
                            InventorySize = "1 Yard",
                            InventoryCode = "GRV1",
                            InventoryListPrice = 5.90m
                        },
                        new Inventory
                        {
                            InventoryDesc = "Topsoil",
                            InventorySize = "1 Yard",
                            InventoryCode = "TSOIL",
                            InventoryListPrice = 12.50m
                        },
                        new Inventory
                        {
                            InventoryDesc = "Patio block-grey",
                            InventorySize = "1 Each",
                            InventoryCode = "PBLKG",
                            InventoryListPrice = 0.84m
                        },
                        new Inventory
                        {
                            InventoryDesc = "Patio block-red",
                            InventorySize = "1 Each",
                            InventoryCode = "PBLKR",
                            InventoryListPrice = 0.84m
                        });
                    context.SaveChanges();
                }

                if (!context.Labours.Any())
                {
                    context.Labours.AddRange(
                        new Labour {
                            LabourType = "Production Worker",
                            LabourPrice = 30.00m,
                            LabourCost = 18.00m
                        },
                        new Labour
                        {
                            LabourType = "Designer",
                            LabourPrice = 65.00m,
                            LabourCost = 40.00m
                        },
                        new Labour
                        {
                            LabourType = "Equipment operator",
                            LabourPrice = 65.00m,
                            LabourCost = 45.00m
                        },
                        new Labour
                        {
                            LabourType = "Botanist",
                            LabourPrice = 75.00m,
                            LabourCost = 50.00m
                        });
                    context.SaveChanges();
                }
                if (!context.ClientRoles.Any())
                {
                    context.ClientRoles.AddRange(
                        new ClientRole
                        {
                            Role = "Owner"
                        },
                        new ClientRole
                        {
                            Role = "Architect"
                        },
                        new ClientRole
                        {
                            Role = "General Contractor"
                        },
                        new ClientRole
                        {
                            Role = "Sub-Contractor"
                        },
                        new ClientRole
                        {
                            Role = "Supplier"
                        });
                    context.SaveChanges();
                }


                // Generate 5 Clients
                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            Name = "John's Construction",
                            ContactFirst = "John",
                            ContactLast = "Smith",
                            Phone = "111-555-5555",
                            Street = "999 Fake Street",
                            City = "Toronto",
                            Province = "ON",
                            Postal = "Z1Z 2Z3",
                            ClientRoleID = 1
                        },
                        new Client
                        {
                            Name = "John's Construction",
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

                            Name = "John's Construction",
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

                            Name = "John's Construction",
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
                            Name = "John's Construction",
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
                        context.SaveChanges();
                }
                // Generate 5 Projects
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(
                        new Project
                        {
                            Start = DateTime.Now,
                            End = DateTime.Now.AddDays(7),
                            Amount = 1000,
                            Created = DateTime.Now,
                            Street = "123 Fake Street",
                            City = "Toronto",
                            Province = "ON",
                            Postal = "Z1Z 1Z1",
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
                        });
                    context.SaveChanges();
                }
                //Generate 5 Bids 
                if(!context.Bids.Any())
                {
                    context.Bids.AddRange(
                        new Bid
                        {
                            DateMade = DateTime.Now,
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "123 Fake Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now,
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "123 Fake Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(12)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "23 Real Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(50)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "99 Real Street").ID
                        },
                        new Bid
                        {

                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(7)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "789 Fake Street").ID
                        });
                        context.SaveChanges();
                }
                //Generate 5 BidMaterials
                if(!context.BidMaterials.Any())
                {
                    context.BidMaterials.AddRange(
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Decorative Cedar bark").ID,
                            InventoryDesc = "Decorative Cedar bark",
                            InventorySize = "5 cu ft",
                            InventoryCode = "CBRK5",
                            InventoryListPrice = 15.95m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Crushed Grantie").ID,
                            InventoryDesc = "Crushed Grantie",
                            InventorySize = "1 Yard",
                            InventoryCode = "CRGRN",
                            InventoryListPrice = 14.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID

                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Pea Gravel").ID,
                            InventoryDesc = "Pea Gravel",
                            InventorySize = "1 Yard",
                            InventoryCode = "PGRV",
                            InventoryListPrice = 20.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.Stage == BidStage.Unapproved).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "1\" Gravel").ID,
                            InventoryDesc = "1\" Gravel",
                            InventorySize = "1 Yard",
                            InventoryCode = "GRV1",
                            InventoryListPrice = 5.90m,
                            BidID = context.Bids.FirstOrDefault(b => b.Stage == BidStage.Unapproved).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Topsoil").ID,
                            InventoryDesc = "Topsoil",
                            InventorySize = "1 Yard",
                            InventoryCode = "TSOIL",
                            InventoryListPrice = 12.50m,
                            BidID = context.Bids.FirstOrDefault(b => b.Stage == BidStage.Unapproved).ID
                        });
                    context.SaveChanges();
                }
                if (!context.BidLabours.Any())
                {
                    context.BidLabours.AddRange(
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Production Worker").ID,
                            LabourType = "Production Worker",
                            LabourPrice = 30.00m,
                            LabourCost = 18.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Designer").ID,
                            LabourType = "Designer",
                            LabourPrice = 65.00m,
                            LabourCost = 40.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Equipment operator").ID,
                            LabourType = "Equipment operator",
                            LabourPrice = 65.00m,
                            LabourCost = 45.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Botanist").ID,
                            LabourType = "Botanist",
                            LabourPrice = 75.00m,
                            LabourCost = 50.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Production Worker").ID,
                            LabourType = "Production Worker",
                            LabourPrice = 30.00m,
                            LabourCost = 18.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 2).ID
                        });
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
