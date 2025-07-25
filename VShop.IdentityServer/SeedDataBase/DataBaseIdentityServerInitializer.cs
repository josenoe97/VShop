﻿using Duende.IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.SeedDataBase
{
    public class DataBaseIdentityServerInitializer : IDataBaseSeedInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataBaseIdentityServerInitializer(UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeSeedRoles()
        {
            // Se o Perfil Admin não existir então cria o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
            {
                //Cria o perfil de Admin
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = IdentityConfiguration.Admin;
                roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();
            }

            // Se o Perfil Cliente não existir então cria o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
            {
                //Cria o perfil de Cliente
                IdentityRole roleClient = new IdentityRole();
                roleClient.Name = IdentityConfiguration.Client;
                roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();
            }
        }

        public void InitializeSeedUsers()
        {
            //se o usuario admin não existir cria o usuario , define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
            {
                //define os dados do usuário admin
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@com.br",
                    NormalizedEmail = "ADMIN1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",
                    FirstName = "Usuario",
                    LastName = "Admin1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Admin e atribui a senha
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Numsey#2022").Result;
                if (resultAdmin.Succeeded)
                {
                    //inclui o usuário admin ao perfil admin
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    //inclui as claims do usuário admin
                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                    }).Result;
                }
            }

            //se o usuario client não existir cria o usuario , define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
            {
                //define os dados do usuário client
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "client1",
                    NormalizedUserName = "CLIENT1",
                    Email = "client1@com.br",
                    NormalizedEmail = "CLIENT1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",
                    FirstName = "Usuario",
                    LastName = "Client1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Client e atribui a senha
                IdentityResult resultClient = _userManager.CreateAsync(client, "Numsey#2022").Result;
                //inclui o usuário Client ao perfil Client
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    //adiciona as claims do usuário Client
                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }
            }
        }
    }
}
