using AspNetCore_SmartPay_Web_API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetCore_SmartPay_Web_API.Services
{
    public class JwtServices
    {

        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _JwtKey;


        //The constructor  for JwtService
        public JwtServices(IConfiguration configuration)
        {
            _configuration = configuration;


            _JwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        }



        public  string CreateJwt(User user)
        {
            //Claim inside  our token


            var userCalims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName)

            };

            var credentials = new SigningCredentials(_JwtKey, SecurityAlgorithms.HmacSha512Signature);


            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userCalims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:ExpiresInDays"])),
                SigningCredentials = credentials,
                Issuer = _configuration["JWT:Issuer"]
            };



            var tokenHandler = new JwtSecurityTokenHandler();

            var jwt = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(jwt);
        }
    }
}
