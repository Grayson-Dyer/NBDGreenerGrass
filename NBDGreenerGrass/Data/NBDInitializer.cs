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
                        },
                        new Inventory
                        {
                            InventoryCode = "PBLKB",
                            InventoryDesc = "Patio block-black",
                            InventorySize = "1 Each",
                            InventoryListPrice = 0.84m
                        },
                        new Inventory
                        {
                            InventoryDesc = "Mulch",
                            InventorySize = "1 Yard",
                            InventoryCode = "MULCH",
                            InventoryListPrice = 10.00m
                        });
                    context.SaveChanges();
                }

                if (!context.Labours.Any())
                {
                    context.Labours.AddRange(
                        new Labour
                        {
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
                        },
                        new Labour
                        {
                            LabourType = "Landscape Architect",
                            LabourPrice = 70.00m,
                            LabourCost = 45.00m
                        },
                        new Labour
                        {
                            LabourType = "Gardener",
                            LabourPrice = 40.00m,
                            LabourCost = 25.00m
                        },
                        new Labour
                        {
                            LabourType = "Irrigation Specialist",
                            LabourPrice = 60.00m,
                            LabourCost = 35.00m
                        },
                        new Labour
                        {
                            LabourType = "Tree Surgeon",
                            LabourPrice = 80.00m,
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
                        },
                        new ClientRole { Role = "Engineer" },
                        new ClientRole { Role = "Interior Designer" },
                        new ClientRole { Role = "Landscape Architect" },
                        new ClientRole { Role = "Surveyor" },
                        new ClientRole { Role = "Project Manager" },
                        new ClientRole { Role = "Quantity Surveyor" },
                        new ClientRole { Role = "Structural Engineer" },
                        new ClientRole { Role = "Electrical Engineer" },
                        new ClientRole { Role = "Mechanical Engineer" },
                        new ClientRole { Role = "Civil Engineer" });
                    context.SaveChanges();
                }


                // Generate 5 Clients
                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            Name = "Hilton Hotel",
                            ContactFirst = "John",
                            ContactLast = "Smith",
                            Phone = "111-555-5555",
                            Street = "999 Fake Street",
                            City = "Toronto",
                            Province = Province.ON,
                            Postal = "Z1Z 2Z3",
                            ClientRoleID = 1
                        },
                        new Client
                        {
                            Name = "Overlook Hotel",
                            ContactFirst = "Jack",
                            ContactLast = "Torrence",
                            Street = "43 Jessica Street",
                            City = "Montreal",
                            Province = Province.QC,
                            Postal = "A3B 3C3",
                            Phone = "999-555-5555",
                            ClientRoleID = 8
                        },
                        new Client
                        {

                            Name = "McDonald",
                            ContactFirst = "Billy",
                            ContactLast = "Joel",
                            Street = "111 Real Street",
                            City = "Toronto",
                            Province = Province.ON,
                            Postal = "A3A 3A3",
                            Phone = "222-555-5555",
                            ClientRoleID = 2
                        },
                        new Client
                        {

                            Name = "Popeye",
                            ContactFirst = "Real",
                            ContactLast = "Person",
                            Street = "321 Electric Avenue",
                            City = "St. Catharines",
                            Province = Province.ON,
                            Postal = "A1A 1A1",
                            Phone = "333-555-5555",
                            ClientRoleID = 1
                        },
                        new Client
                        {
                            Name = "Fake's Construction",
                            ContactFirst = "Bob",
                            ContactLast = "Ross",
                            Street = "123 Fake Street",
                            City = "Kingston",
                            Province = Province.ON,
                            Postal = "A1B 2C3",
                            Phone = "416-555-5555",
                            ClientRoleID = 1
                        },
                        new Client
                        {
                            Name = "Burger King",
                            ContactFirst = "Peter",
                            ContactLast = "Parker",
                            Street = "456 Spider Street",
                            City = "New York",
                            Province = Province.ON,
                            Postal = "10001",
                            Phone = "444-555-5555",
                            ClientRoleID = 3
                        },
                        new Client
                        {
                            Name = "Starbucks",
                            ContactFirst = "Tony",
                            ContactLast = "Stark",
                            Street = "789 Iron Street",
                            City = "Los Angeles",
                            Province = Province.ON,
                            Postal = "90001",
                            Phone = "555-555-5555",
                            ClientRoleID = 4
                        },
                        new Client
                        {
                            Name = "Subway",
                            ContactFirst = "Bruce",
                            ContactLast = "Wayne",
                            Street = "123 Bat Street",
                            City = "Gotham",
                            Province = Province.NT,
                            Postal = "10002",
                            Phone = "666-555-5555",
                            ClientRoleID = 5
                        },
                        new Client
                        {
                            Name = "KFC",
                            ContactFirst = "Clark",
                            ContactLast = "Kent",
                            Street = "456 Super Street",
                            City = "Metropolis",
                            Province = Province.YT,
                            Postal = "61801",
                            Phone = "777-555-5555",
                            ClientRoleID = 6
                        },
                        new Client
                        {
                            Name = "Pizza Hut",
                            ContactFirst = "Diana",
                            ContactLast = "Prince",
                            Street = "789 Wonder Street",
                            City = "Washington",
                            Province = Province.ON,
                            Postal = "20001",
                            Phone = "888-555-5555",
                            ClientRoleID = 7
                        },
                        new Client
                        {
                            Name = "Taco Bell",
                            ContactFirst = "Barry",
                            ContactLast = "Allen",
                            Street = "123 Flash Street",
                            City = "Central City",
                            Province = Province.QC,
                            Postal = "64101",
                            Phone = "999-555-5555",
                            ClientRoleID = 8
                        },
                        new Client
                        {
                            Name = "Dunkin Donuts",
                            ContactFirst = "Arthur",
                            ContactLast = "Curry",
                            Street = "456 Aqua Street",
                            City = "Amnesty Bay",
                            Province = Province.BC,
                            Postal = "04606",
                            Phone = "101-555-5555",
                            ClientRoleID = 9
                        },
                        new Client
                        {
                            Name = "Domino's Pizza",
                            ContactFirst = "Hal",
                            ContactLast = "Jordan",
                            Street = "789 Lantern Street",
                            City = "Coast City",
                            Province = Province.PE,
                            Postal = "90002",
                            Phone = "102-555-5555",
                            ClientRoleID = 10
                        },
                        new Client
                        {
                            Name = "McDonald's",
                            ContactFirst = "Oliver",
                            ContactLast = "Queen",
                            Street = "123 Arrow Street",
                            City = "Star City",
                            Province = Province.ON,
                            Postal = "90003",
                            Phone = "103-555-5555",
                            ClientRoleID = 11
                        },
                        new Client
                        {
                            Name = "Wendy's",
                            ContactFirst = "Wally",
                            ContactLast = "West",
                            Street = "456 Kid Street",
                            City = "Keystone City",
                            Province = Province.AB,
                            Postal = "66101",
                            Phone = "104-555-5555",
                            ClientRoleID = 12
                        },
                        new Client
                        {
                            Name = "Acme Corporation",
                            ContactFirst = "Wile",
                            ContactLast = "Coyote",
                            Street = "123 Desert Street",
                            City = "Phoenix",
                            Province = Province.SK,
                            Postal = "85001",
                            Phone = "105-555-5555",
                            ClientRoleID = 13
                        },
                        new Client
                        {
                            Name = "Stark Industries",
                            ContactFirst = "Tony",
                            ContactLast = "Stark",
                            Street = "456 Iron Street",
                            City = "Los Angeles",
                            Province = Province.BC,
                            Postal = "90001",
                            Phone = "106-555-5555",
                            ClientRoleID = 14
                        },
                        new Client
                        {
                            Name = "Wayne Enterprises",
                            ContactFirst = "Bruce",
                            ContactLast = "Wayne",
                            Street = "789 Bat Street",
                            City = "Gotham",
                            Province = Province.ON,
                            Postal = "10002",
                            Phone = "107-555-5555",
                            ClientRoleID = 15
                        },
                        new Client
                        {
                            Name = "Oscorp Industries",
                            ContactFirst = "Norman",
                            ContactLast = "Osborn",
                            Street = "123 Goblin Street",
                            City = "New York",
                            Province = Province.QC,
                            Postal = "10001",
                            Phone = "108-555-5555",
                            ClientRoleID = 15
                        },
                        new Client
                        {
                            Name = "LexCorp Industries",
                            ContactFirst = "Lex",
                            ContactLast = "Luthor",
                            Street = "456 Super Street",
                            City = "Metropolis",
                            Province = Province.NS,
                            Postal = "61801",
                            Phone = "109-555-5555",
                            ClientRoleID = 12
                        },
                        new Client
                        {
                            Name = "Pym Technologies",
                            ContactFirst = "Hank",
                            ContactLast = "Pym",
                            Street = "789 Ant Street",
                            City = "San Francisco",
                            Province = Province.ON,
                            Postal = "94101",
                            Phone = "110-555-5555",
                            ClientRoleID = 7
                        },
                        new Client
                        {
                            Name = "Queen Consolidated",
                            ContactFirst = "Oliver",
                            ContactLast = "Queen",
                            Street = "123 Arrow Street",
                            City = "Star City",
                            Province = Province.NU,
                            Postal = "90003",
                            Phone = "111-555-5555",
                            ClientRoleID = 3
                        },
                        new Client
                        {
                            Name = "Rand Enterprises",
                            ContactFirst = "Danny",
                            ContactLast = "Rand",
                            Street = "456 Fist Street",
                            City = "New York",
                            Province = Province.MB,
                            Postal = "10001",
                            Phone = "112-555-5555",
                            ClientRoleID = 1
                        },
                        new Client
                        {
                            Name = "Kord Industries",
                            ContactFirst = "Ted",
                            ContactLast = "Kord",
                            Street = "789 Beetle Street",
                            City = "Chicago",
                            Province = Province.ON,
                            Postal = "60601",
                            Phone = "113-555-5555",
                            ClientRoleID = 10
                        },
                        new Client
                        {
                            Name = "S.T.A.R. Labs",
                            ContactFirst = "Harrison",
                            ContactLast = "Wells",
                            Street = "123 Flash Street",
                            City = "Central City",
                            Province = Province.SK,
                            Postal = "64101",
                            Phone = "114-555-5555",
                            ClientRoleID = 11
                        });
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
                            Province = Province.ON,
                            Postal = "Z1Z 1Z1",
                            ClientID = context.Clients.FirstOrDefault(c => c.ContactFirst == "Jack").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now,
                            End = DateTime.Now.AddDays(7),
                            City = "Montreal",
                            Province = Province.QC,
                            Postal = "A1A 1A1",
                            Street = "456 Fake Street",
                            Created = DateTime.Now,
                            Amount = 2000,
                            ClientID = context.Clients.FirstOrDefault(c => c.ContactFirst == "Billy").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now,
                            End = DateTime.Now.AddDays(7),
                            City = "Toronto",
                            Province = Province.ON,
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
                            Province = Province.ON,
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
                            Province = Province.ON,
                            Postal = "Z0Z 0Z0",
                            Street = "789 Fake Street",
                            City = "Toronto",
                            Created = DateTime.Now,
                            Amount = 3000,
                            ClientID = context.Clients.FirstOrDefault(c => c.ContactFirst == "Bob").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 5000,
                            Created = DateTime.Now,
                            Street = "123 Acme Street",
                            City = "Phoenix",
                            Province = Province.AB,
                            Postal = "85001",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Acme Corporation").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 6000,
                            Created = DateTime.Now,
                            Street = "456 Stark Street",
                            City = "Los Angeles",
                            Province = Province.SK,
                            Postal = "90001",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Stark Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 7000,
                            Created = DateTime.Now,
                            Street = "789 Wayne Street",
                            City = "Gotham",
                            Province = Province.SK,
                            Postal = "10002",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Wayne Enterprises").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 8000,
                            Created = DateTime.Now,
                            Street = "123 Oscorp Street",
                            City = "New York",
                            Province = Province.NL,
                            Postal = "10001",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Oscorp Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 9000,
                            Created = DateTime.Now,
                            Street = "456 LexCorp Street",
                            City = "Metropolis",
                            Province = Province.BC,
                            Postal = "61801",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "LexCorp Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 10000,
                            Created = DateTime.Now,
                            Street = "789 Pym Street",
                            City = "San Francisco",
                            Province = Province.ON,
                            Postal = "94101",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Pym Technologies").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 11000,
                            Created = DateTime.Now,
                            Street = "123 Queen Street",
                            City = "Star City",
                            Province = Province.YT,
                            Postal = "90003",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Queen Consolidated").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 12000,
                            Created = DateTime.Now,
                            Street = "456 Rand Street",
                            City = "New York",
                            Province = Province.BC,
                            Postal = "10001",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Rand Enterprises").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 13000,
                            Created = DateTime.Now,
                            Street = "789 Kord Street",
                            City = "Chicago",
                            Province = Province.NU,
                            Postal = "60601",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Kord Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 14000,
                            Created = DateTime.Now,
                            Street = "123 STAR Street",
                            City = "Central City",
                            Province = Province.YT,
                            Postal = "64101",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "S.T.A.R. Labs").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 15000,
                            Created = DateTime.Now,
                            Street = "321 Acme Street",
                            City = "Phoenix",
                            Province = Province.PE,
                            Postal = "85002",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Acme Corporation").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 16000,
                            Created = DateTime.Now,
                            Street = "654 Stark Street",
                            City = "Los Angeles",
                            Province = Province.AB,
                            Postal = "90002",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Stark Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 17000,
                            Created = DateTime.Now,
                            Street = "987 Wayne Street",
                            City = "Gotham",
                            Province = Province.BC,
                            Postal = "10003",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Wayne Enterprises").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 18000,
                            Created = DateTime.Now,
                            Street = "321 Oscorp Street",
                            City = "New York",
                            Province =  Province.NT,
                            Postal = "10002",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Oscorp Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 19000,
                            Created = DateTime.Now,
                            Street = "654 LexCorp Street",
                            City = "Metropolis",
                            Province = Province.AB,
                            Postal = "61802",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "LexCorp Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 20000,
                            Created = DateTime.Now,
                            Street = "987 Pym Street",
                            City = "San Francisco",
                            Province = Province.PE,
                            Postal = "94102",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Pym Technologies").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 21000,
                            Created = DateTime.Now,
                            Street = "321 Queen Street",
                            City = "Star City",
                            Province = Province.ON,
                            Postal = "90004",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Queen Consolidated").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 22000,
                            Created = DateTime.Now,
                            Street = "654 Rand Street",
                            City = "New York",
                            Province = Province.YT,
                            Postal = "10002",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Rand Enterprises").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 23000,
                            Created = DateTime.Now,
                            Street = "987 Kord Street",
                            City = "Chicago",
                            Province = Province.ON,
                            Postal = "60602",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Kord Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 24000,
                            Created = DateTime.Now,
                            Street = "321 STAR Street",
                            City = "Central City",
                            Province = Province.ON,
                            Postal = "64102",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "S.T.A.R. Labs").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 25000,
                            Created = DateTime.Now,
                            Street = "123 Acme Street",
                            City = "Phoenix",
                            Province = Province.ON,
                            Postal = "85003",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Acme Corporation").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 26000,
                            Created = DateTime.Now,
                            Street = "456 Stark Street",
                            City = "Los Angeles",
                            Province = Province.ON,
                            Postal = "90003",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Stark Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 27000,
                            Created = DateTime.Now,
                            Street = "789 Wayne Street",
                            City = "Gotham",
                            Province = Province.ON,
                            Postal = "10004",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Wayne Enterprises").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 28000,
                            Created = DateTime.Now,
                            Street = "123 Oscorp Street",
                            City = "New York",
                            Province = Province.QC,
                            Postal = "10003",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "Oscorp Industries").ID
                        },
                        new Project
                        {
                            Start = DateTime.Now.AddDays(new Random().Next(1, 30)),
                            End = DateTime.Now.AddDays(new Random().Next(31, 60)),
                            Amount = 29000,
                            Created = DateTime.Now,
                            Street = "456 LexCorp Street",
                            City = "Metropolis",
                            Province = Province.ON,
                            Postal = "61803",
                            ClientID = context.Clients.FirstOrDefault(c => c.Name == "LexCorp Industries").ID
                        });
                    context.SaveChanges();
                }
                //Generate 5 Bids 
                if (!context.Bids.Any())
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
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(12)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "123 Acme Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(34)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "456 Stark Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(23)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "789 Wayne Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(45)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "123 Oscorp Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(56)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "456 LexCorp Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(67)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "789 Pym Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(10)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "123 Queen Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(20)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "456 Rand Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(15)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "789 Kord Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(4)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "321 STAR Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(12)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "321 STAR Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(23)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "123 Queen Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(34)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "456 Rand Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(45)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "789 Kord Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(56)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "789 Kord Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(67)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "789 Kord Street").ID
                        },
                        new Bid
                        {
                            DateMade = DateTime.Now.Subtract(TimeSpan.FromDays(10)),
                            Stage = BidStage.Unapproved,
                            ProjectID = context.Projects.FirstOrDefault(p => p.Street == "123 Queen Street").ID
                        });
                    context.SaveChanges();
                }
                //Generate 5 BidMaterials
                if (!context.BidMaterials.Any())
                {
                    context.BidMaterials.AddRange(
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Decorative Cedar bark").ID,
                            InventoryDesc = "Decorative Cedar bark",
                            InventorySize = "5 cu ft",
                            InventoryCode = "CBRK5",
                            InventoryListPrice = 15.95m,
                            Quantity = 3,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Crushed Grantie").ID,
                            InventoryDesc = "Crushed Grantie",
                            InventorySize = "1 Yard",
                            InventoryCode = "CRGRN",
                            Quantity = 2,
                            InventoryListPrice = 14.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID

                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Pea Gravel").ID,
                            InventoryDesc = "Pea Gravel",
                            InventorySize = "1 Yard",
                            InventoryCode = "PGRV",
                            Quantity = 1,
                            InventoryListPrice = 20.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 2).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "1\" Gravel").ID,
                            InventoryDesc = "1\" Gravel",
                            InventorySize = "1 Yard",
                            InventoryCode = "GRV1",
                            Quantity = 4,
                            InventoryListPrice = 5.90m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 3).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Topsoil").ID,
                            InventoryDesc = "Topsoil",
                            InventorySize = "1 Yard",
                            InventoryCode = "TSOIL",
                            Quantity = 2,
                            InventoryListPrice = 12.50m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 4).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Decorative Cedar bark").ID,
                            InventoryDesc = "Decorative Cedar bark",
                            InventorySize = "5 cu ft",
                            InventoryCode = "CBRK5",
                            Quantity = 3,
                            InventoryListPrice = 15.95m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 4).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Crushed Grantie").ID,
                            InventoryDesc = "Crushed Grantie",
                            InventorySize = "1 Yard",
                            InventoryCode = "CRGRN",
                            Quantity = 2,
                            InventoryListPrice = 14.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 6).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Pea Gravel").ID,
                            InventoryDesc = "Pea Gravel",
                            InventorySize = "1 Yard",
                            InventoryCode = "PGRV",
                            Quantity = 1,
                            InventoryListPrice = 20.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 7).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "1\" Gravel").ID,
                            InventoryDesc = "1\" Gravel",
                            InventorySize = "1 Yard",
                            InventoryCode = "GRV1",
                            Quantity = 4,
                            InventoryListPrice = 5.90m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 8).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Topsoil").ID,
                            InventoryDesc = "Topsoil",
                            InventorySize = "1 Yard",
                            InventoryCode = "TSOIL",
                            Quantity = 2,
                            InventoryListPrice = 12.50m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 9).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Decorative Cedar bark").ID,
                            InventoryDesc = "Mulch",
                            InventorySize = "1 Yard",
                            InventoryCode = "MULCH",
                            Quantity = 3,
                            InventoryListPrice = 15.95m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 10).ID
                        },
                        new BidMaterial
                        {
                            InventoryID = context.Inventories.FirstOrDefault(i => i.InventoryDesc == "Crushed Grantie").ID,
                            InventoryDesc = "Crushed Grantie",
                            InventorySize = "1 Yard",
                            InventoryCode = "CRGRN",
                            Quantity = 2,
                            InventoryListPrice = 14.00m,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 10).ID
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
                            HoursWorked = 10,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Designer").ID,
                            LabourType = "Designer",
                            LabourPrice = 65.00m,
                            LabourCost = 40.00m,
                            HoursWorked = 5,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Equipment operator").ID,
                            LabourType = "Equipment operator",
                            LabourPrice = 65.00m,
                            LabourCost = 45.00m,
                            HoursWorked = 8,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 1).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Botanist").ID,
                            LabourType = "Botanist",
                            LabourPrice = 75.00m,
                            LabourCost = 50.00m,
                            HoursWorked = 6,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 2).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Production Worker").ID,
                            LabourType = "Production Worker",
                            LabourPrice = 30.00m,
                            LabourCost = 18.00m,
                            HoursWorked = 10,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 2).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Designer").ID,
                            LabourType = "Designer",
                            LabourPrice = 65.00m,
                            LabourCost = 40.00m,
                            HoursWorked = 5,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 3).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Equipment operator").ID,
                            LabourType = "Equipment operator",
                            LabourPrice = 65.00m,
                            LabourCost = 45.00m,
                            HoursWorked = 8,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 3).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Botanist").ID,
                            LabourType = "Botanist",
                            LabourPrice = 75.00m,
                            LabourCost = 50.00m,
                            HoursWorked = 6,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 4).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Production Worker").ID,
                            LabourType = "Production Worker",
                            LabourPrice = 30.00m,
                            LabourCost = 18.00m,
                            HoursWorked = 10,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 4).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Designer").ID,
                            LabourType = "Designer",
                            LabourPrice = 65.00m,
                            LabourCost = 40.00m,
                            HoursWorked = 5,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 5).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Equipment operator").ID,
                            LabourType = "Equipment operator",
                            LabourPrice = 65.00m,
                            LabourCost = 45.00m,
                            HoursWorked = 8,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 5).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Botanist").ID,
                            LabourType = "Botanist",
                            LabourPrice = 75.00m,
                            LabourCost = 50.00m,
                            HoursWorked = 6,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 6).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Production Worker").ID,
                            LabourType = "Production Worker",
                            LabourPrice = 30.00m,
                            LabourCost = 18.00m,
                            HoursWorked = 10,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 6).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Designer").ID,
                            LabourType = "Designer",
                            LabourPrice = 65.00m,
                            LabourCost = 40.00m,
                            HoursWorked = 5,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 7).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Equipment operator").ID,
                            LabourType = "Equipment operator",
                            LabourPrice = 65.00m,
                            LabourCost = 45.00m,
                            HoursWorked = 8,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 7).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Botanist").ID,
                            LabourType = "Botanist",
                            LabourPrice = 75.00m,
                            LabourCost = 50.00m,
                            HoursWorked = 6,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 7).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Production Worker").ID,
                            LabourType = "Production Worker",
                            LabourPrice = 30.00m,
                            LabourCost = 18.00m,
                            HoursWorked = 10,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 8).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Designer").ID,
                            LabourType = "Designer",
                            LabourPrice = 65.00m,
                            LabourCost = 40.00m,
                            HoursWorked = 5,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 8).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Equipment operator").ID,
                            LabourType = "Equipment operator",
                            LabourPrice = 65.00m,
                            LabourCost = 45.00m,
                            HoursWorked = 8,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 9).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Botanist").ID,
                            LabourType = "Botanist",
                            LabourPrice = 75.00m,
                            LabourCost = 50.00m,
                            HoursWorked = 6,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 9).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Production Worker").ID,
                            LabourType = "Production Worker",
                            LabourPrice = 30.00m,
                            LabourCost = 18.00m,
                            HoursWorked = 10,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 9).ID
                        },
                        new BidLabour
                        {
                            LabourID = context.Labours.FirstOrDefault(l => l.LabourType == "Designer").ID,
                            LabourType = "Designer",
                            LabourPrice = 65.00m,
                            LabourCost = 40.00m,
                            HoursWorked = 5,
                            BidID = context.Bids.FirstOrDefault(b => b.ID == 10).ID
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
