using AspNetCore_SmartPay_Web_API.Data;
using AspNetCore_SmartPay_Web_API.Models;
using AspNetCore_SmartPay_Web_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//***Configuration the Connections String using defalutconnection to add 
builder.Services.AddDbContext<SmartpayDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

);

//***JwtServices to able to inject jwt service class on out controllers
builder.Services.AddScoped<JwtServices>();



//***Defining our IdentityCore Services

builder.Services.AddIdentityCore<User>(option =>
{
    option.Password.RequiredLength = 6;
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireNonAlphanumeric = false;



    //email confirmation
    option.SignIn.RequireConfirmedEmail = true;
})
.AddRoles<IdentityRole>()                           //Used to Add Roles
.AddRoleManager<RoleManager<IdentityRole>>()        //To make use of Role Manager
.AddEntityFrameworkStores<SmartpayDbContext>()      //Provide our Context
.AddSignInManager<SignInManager<User>>()            //make use of signin manager
.AddUserManager<UserManager<User>>()                // able to create users
.AddDefaultTokenProviders();                       //to create some tokens for email confirmation




//*** To authenticate the use using JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
   
                                                                          {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           //validate the token based on the key I provided inside appsetting.development.json JWT:Key
           ValidateIssuerSigningKey = true,

           //The Issuer signing key based on JWT:Key
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        
          // The issuer which is here the api  project url Im using in this project
           ValidIssuer = builder.Configuration["JWT:Issuer"],
           //validate issuer (who ever is using the jwt)

           ValidateIssuer = true,


           ValidateAudience = false,
          


       };

   });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

//app.UseCors("AllowAll");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //***  adding use Authentication into our pipeline , will be used to varify the identity of a use 
app.UseAuthorization();//determines user access rights

app.MapControllers();

app.Run();
