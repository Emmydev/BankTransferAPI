using BankTransfer.BLL.Constants;
using BankTransfer.Contracts;
using BankTransfer.Contracts.Authentication;
using BankTransfer.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.BLL.Services.Authentication
{
    public class IdentityService: IIdentityService
    {
        private readonly DataContext context;
        private readonly IConfiguration config;

        public IdentityService(DataContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        public async Task<APIResponse<AuthDetails>> Login(LoginDetails request)
        {
            var response = new APIResponse<AuthDetails>();
            try
            {
                var organization = await context.Organization.FirstOrDefaultAsync(x => x.ClientId == request.ClientId && 
                x.ClientSecret.ToLower() == request.ClientSecret.ToLower() && 
                x.Deleted !=true);
                if (organization == null)
                {
                    response.IsSuccessful = false;
                    response.Error.Code = Codes.InvalidInput;
                    response.Error.Description = "Invalid Login Details";
                    return response;
                }
                var tokenDetails = await GenerateAuthenticationToken(organization.ClientId, organization.Email);
                response.IsSuccessful = true;
                response.Result = tokenDetails;
                return response;
            }
            catch(Exception ex)
            {
                ///>> log error
                response.IsSuccessful = false;
                response.Error.Code = Codes.SystemMalfunction;
                response.Error.Description = Messages.FriendlyException;
                return response;
            }
        }

        private async Task<AuthDetails> GenerateAuthenticationToken(int clientId, string Email)
        {
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, clientId.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(JwtRegisteredClaimNames.Email, Email),
               new Claim("clientId", clientId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthDetails { Token = tokenHandler.WriteToken(token), Expires = tokenDescriptor.Expires.ToString() };
        }
    }
}
