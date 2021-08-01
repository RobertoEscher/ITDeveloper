using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.ExtensionsMethods
{
    public static class ModelBuilderExtension
    {

        const string NOMECOMPLETO = "Roberto França";
        const string APELIDO = "rfranca";
        //DateTime DATANASCIMENTO = DateTime.Now;
        const string EMAIL = "rfranca@gmail.com";
        const string PASSWORD = "Admin@123";
        const string ROLENAME = "Admin";
        const string USERNAME = EMAIL;
        const string ROLEID = "762818A5-EC7E-4CFF-A74E-B9103E92F021";
        const string USERID = "0D39D367-3E94-4AD8-A49F-519B2A3F358D";


        public static ModelBuilder AddUserAndRole(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLEID,
                Name = ROLENAME,
                NormalizedName = ROLENAME.ToUpper()
            });


            var phash = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = USERID,
                Apelido = APELIDO,
                NomeCompleto = NOMECOMPLETO,
                DataNascimento = DateTime.Now,
                Email = EMAIL,
                NormalizedEmail = EMAIL.ToUpper(),
                UserName = USERNAME,
                NormalizedUserName = USERNAME.ToUpper(),
                PasswordHash = phash.HashPassword(null, PASSWORD),
                EmailConfirmed = true
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLEID, 
                UserId = USERID
            });

            
            return builder;
        }

        public static ModelBuilder AddGericos(this ModelBuilder builder)
        {
            var k = 0;
            string line;

#pragma warning disable SYSLIB0012 // Type or member is obsolete
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
#pragma warning restore SYSLIB0012 // Type or member is obsolete
            var csvPath = Path.Combine(outPutDirectory, "Csv\\Generico.CSV");
            string filePath = new Uri(csvPath).LocalPath;

            using(var fs = File.OpenRead(filePath))
            using(var reader = new StreamReader(fs))

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    var codigo = parts[0];
                    var generico = parts[1];

                    if (k > 0)
                    {
                        builder.Entity<Generico>().HasData(new Generico
                        {
                            Codigo = Convert.ToInt32(codigo),
                            Nome = generico

                        });
                    }

                    k++;
                }


                return builder;
        }

    }
}
