using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Business.DependencyResolvers.Microsoft;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configurations = builder.Configuration;
builder.Services.AddDependencies(configurations);
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();

var app = builder.Build();
app.UseAuthentication();
app.MapDefaultControllerRoute();


app.Run();
