namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ATWPJWebService.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ATWPJWebService.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            //Delete all rows and reset id to zero for seeding
            context.Database.ExecuteSqlCommand("DELETE FROM Reports");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Reports', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DELETE FROM Photos");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Photos', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DELETE FROM Trips");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Trips', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DELETE FROM Requests");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Requests', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DELETE FROM AspNetUsers");

    

            context.Users.AddOrUpdate(i => i.Id,
                new Models.ApplicationUser
                {
                    Id = "1fdea673-a5bd-435a-bdc2-0931a29b04b2",
                    PasswordHash = "AHcaA2Vc7xK5T8XgO3Cj02eBfOqTgKiOedBaT/JY2VqvkloHfOYc2w3wCPqP83GWPw==",
                    SecurityStamp = "da3e431e-5583-437c-ac27-dac743851908",
                    Email = "flo@flo.at",
                    UserName = "flo@flo.at",
                    FirstName = "Florian",
                    LastName = "Gasser",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                },

                new Models.ApplicationUser
                {
                    Id = "44a6f4ae-0c4d-4dbb-bdd6-6cebfae8c2e0",
                    PasswordHash = "AKY0U9WUdC8Rk52UjKFOzHkGDACgMytaNNuqORn8PjSyMjB/NjEzAmQjwo2SdoVcbA==",
                    SecurityStamp = "ae62afac-fcf8-4f63-949f-d4e10827ca68",
                    Email = "nastja@nastja.at",
                    UserName = "nastja@nastja.at",
                    FirstName = "Nastja",
                    LastName = "Fischtschenko",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                },

                new Models.ApplicationUser
                {
                    Id = "94995bdb-0b3d-41b9-ba35-e7f46f98fac5",
                    PasswordHash = "AHRjx6q8DLuC9moe3Z/irz3dKQDmGzNHZzztW72YErb+m5AxdeyrjrLqAMRxIA/UgQ==",
                    SecurityStamp = "6b764df5-e289-48e2-ae86-9fcfd5b6546a",
                    Email = "michael@michael.at",
                    UserName = "michael@michael.at",
                    FirstName = "Michael",
                    LastName = "Kalteis",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                },

                new Models.ApplicationUser
                {
                    Id = "f8fccb01-ddc4-432e-abd5-f64f7812e1de",
                    PasswordHash = "ACTjEqxgJ6cC9Tr73l+4PbxvpokWjgFrOwXvLajt87LYNt5VdROofqGW2Te7Vobaww==",
                    SecurityStamp = "a8dbbf5d-620c-4eba-b42f-c71ba33ba800",
                    Email = "matthias@matthias.at",
                    UserName = "matthias@matthias.at",
                    FirstName = "Matthias",
                    LastName = "Kerschbaum",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                }
            );

            context.SaveChanges();

            context.Trips.AddOrUpdate(i => i.Title,
                new Models.Trip
                {
                    Title = "NY 2014",
                    CreationDate = DateTime.Now,
                    IsPrivate = false,
                    UserId = "1fdea673-a5bd-435a-bdc2-0931a29b04b2"
                },

                new Models.Trip
                {
                    Title = "LA 2015",
                    CreationDate = DateTime.Now,
                    IsPrivate = false,
                    UserId = "1fdea673-a5bd-435a-bdc2-0931a29b04b2"
                },

                new Models.Trip
                {
                    Title = "Wien 2017",
                    CreationDate = DateTime.Now,
                    IsPrivate = false,
                    UserId = "f8fccb01-ddc4-432e-abd5-f64f7812e1de"
                },

                new Models.Trip
                {
                    Title = "Salzburg 2015",
                    CreationDate = DateTime.Now,
                    IsPrivate = false,
                    UserId = "f8fccb01-ddc4-432e-abd5-f64f7812e1de"
                }
            );

            context.SaveChanges();

            context.Photos.AddOrUpdate(i => i.FileName,
                new Models.Photo
                {
                    FileName = @"C:\Photos\photo1.jpg",
                    Longitude = -73.935242f,
                    Latitude = 40.730610f,
                    TripId = 1
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo2.jpg",
                    Longitude = -73.935242f,
                    Latitude = 40.730610f,
                    TripId = 1
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo3.jpg",
                    Longitude = -73.935242f,
                    Latitude = 40.730610f,
                    TripId = 1
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo4.jpg",
                    Longitude = -73.935242f,
                    Latitude = 40.730610f,
                    TripId = 1
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo5.jpg",
                    Longitude = -118.243683f,
                    Latitude = 34.052235f,
                    TripId = 2
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo6.jpg",
                    Longitude = -118.243683f,
                    Latitude = 34.052235f,
                    TripId = 2
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo7.jpg",
                    Longitude = -118.243683f,
                    Latitude = 34.052235f,
                    TripId = 2
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo8.jpg",
                    Longitude = -118.243683f,
                    Latitude = 34.052235f,
                    TripId = 2
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo9.jpg",
                    Longitude = 16.363449f,
                    Latitude = 48.210033f,
                    TripId = 3
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo10.jpg",
                    Longitude = 16.363449f,
                    Latitude = 48.210033f,
                    TripId = 3
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo11.jpg",
                    Longitude = 16.363449f,
                    Latitude = 48.210033f,
                    TripId = 3
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo12.jpg",
                    Longitude = 16.363449f,
                    Latitude = 48.210033f,
                    TripId = 3
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo13.jpg",
                    Longitude = 13.033229f,
                    Latitude = 47.811195f,
                    TripId = 4
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo14.jpg",
                    Longitude = 13.033229f,
                    Latitude = 47.811195f,
                    TripId = 4
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo15.jpg",
                    Longitude = 13.033229f,
                    Latitude = 47.811195f,
                    TripId = 4
                },

                new Models.Photo
                {
                    FileName = @"C:\Photos\photo16.jpg",
                    Longitude = 13.033229f,
                    Latitude = 47.811195f,
                    TripId = 4
                }
            );

            context.SaveChanges();

            context.Requests.AddOrUpdate(i => new { i.RequestFromUserId, i.RequestToUserId },
                new Models.Request
                {
                    RequestFromUserId = "1fdea673-a5bd-435a-bdc2-0931a29b04b2",
                    RequestToUserId = "f8fccb01-ddc4-432e-abd5-f64f7812e1de",
                    IsNew = false,
                    IsAccepted = true,
                    CreationDate = DateTime.Now
                },

                new Models.Request
                {
                    RequestFromUserId = "f8fccb01-ddc4-432e-abd5-f64f7812e1de",
                    RequestToUserId = "1fdea673-a5bd-435a-bdc2-0931a29b04b2",
                    IsNew = false,
                    IsAccepted = true,
                    CreationDate = DateTime.Now
                },

                new Models.Request
                {
                    RequestFromUserId = "44a6f4ae-0c4d-4dbb-bdd6-6cebfae8c2e0",
                    RequestToUserId = "1fdea673-a5bd-435a-bdc2-0931a29b04b2",
                    IsNew = false,
                    IsAccepted = true,
                    CreationDate = DateTime.Now
                },

                new Models.Request
                {
                    RequestFromUserId = "44a6f4ae-0c4d-4dbb-bdd6-6cebfae8c2e0",
                    RequestToUserId = "f8fccb01-ddc4-432e-abd5-f64f7812e1de",
                    IsNew = true,
                    IsAccepted = false,
                    CreationDate = DateTime.Now
                },

                new Models.Request
                {
                    RequestFromUserId = "44a6f4ae-0c4d-4dbb-bdd6-6cebfae8c2e0",
                    RequestToUserId = "94995bdb-0b3d-41b9-ba35-e7f46f98fac5",
                    IsNew = false,
                    IsAccepted = false,
                    CreationDate = DateTime.Now
                },

                new Models.Request
                {
                    RequestFromUserId = "94995bdb-0b3d-41b9-ba35-e7f46f98fac5",
                    RequestToUserId = "1fdea673-a5bd-435a-bdc2-0931a29b04b2",
                    IsNew = true,
                    IsAccepted = false,
                    CreationDate = DateTime.Now
                },

                new Models.Request
                {
                    RequestFromUserId = "94995bdb-0b3d-41b9-ba35-e7f46f98fac5",
                    RequestToUserId = "44a6f4ae-0c4d-4dbb-bdd6-6cebfae8c2e0",
                    IsNew = true,
                    IsAccepted = false,
                    CreationDate = DateTime.Now
                },

                new Models.Request
                {
                    RequestFromUserId = "94995bdb-0b3d-41b9-ba35-e7f46f98fac5",
                    RequestToUserId = "f8fccb01-ddc4-432e-abd5-f64f7812e1de",
                    IsNew = true,
                    IsAccepted = false,
                    CreationDate = DateTime.Now
                }
            );

            context.SaveChanges();

            context.Reports.AddOrUpdate(i => new { i.PhotoId, i.UserId},
                new Models.Report
                {
                    PhotoId = 1,
                    UserId = "94995bdb-0b3d-41b9-ba35-e7f46f98fac5",
                    IsDone = false
                },

                new Models.Report
                {
                    PhotoId = 4,
                    UserId = "94995bdb-0b3d-41b9-ba35-e7f46f98fac5",
                    IsDone = false
                },

                new Models.Report
                {
                    PhotoId = 7,
                    UserId = "44a6f4ae-0c4d-4dbb-bdd6-6cebfae8c2e0",
                    IsDone = false
                }
            );

            context.SaveChanges();

        }
    }
}
