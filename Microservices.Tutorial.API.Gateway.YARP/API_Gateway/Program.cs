using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography.Xml;
using Yarp.ReverseProxy.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
 .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
//  .LoadFromMemory(
// [
//    new RouteConfig
//    {
//         RouteId = "API1-Route",
//         ClusterId  = "API1-Cluster",
//         Match = new(){ Path = "/api1/{**catch-all}" },
//         Transforms = new List<Dictionary<string, string>>
//         {
//            new()
//            {
//                ["RequestHeader"] = "api1-request-header",
//                ["Append"] = "api1 request"
//            },
//               new()
//            {
//                { "ResponseHeader", "api1-response-header" },
//                { "Append", "api1 response" },
//                { "When", "Always" },
//            }
//         }
//    },
//    new RouteConfig
//    {
//         RouteId = "API2-Route",
//         ClusterId  = "API2-Cluster",
//         Match = new(){ Path = "/api2/{**catch-all}" },
//         Transforms = new List<Dictionary<string, string>>
//         {
//            new()
//            {
//                ["RequestHeader"] = "api2-request-header",
//                ["Append"] = "api2 request"
//            },
//               new()
//            {
//                { "ResponseHeader", "api2-response-header" },
//                { "Append", "api2 response" },
//                { "When", "Always" },
//            }
//         }
//    },
//    new RouteConfig
//    {
//         RouteId = "API3-Route",
//         ClusterId  = "API3-Cluster",
//         Match = new(){ Path = "/api3/{**catch-all}"},
//         Transforms = new List<Dictionary<string, string>>
//         {
//            new()
//            {
//                ["RequestHeader"] = "api3-request-header",
//                ["Append"] = "api3 request"
//            },
//               new()
//            {
//                { "ResponseHeader", "api3-response-header" },
//                { "Append", "api3 response" },
//                { "When", "Always" },
//            }
//         }
//    }
// ],
// [
//    new ClusterConfig()
//    {
//        ClusterId = "API1-Cluster",
//        Destinations = new Dictionary<string, DestinationConfig>
//        {
//            ["destination1"] = new(){ Address = "https://localhost:5001"}
//        }
//    },
//    new ClusterConfig()
//    {
//        ClusterId = "API2-Cluster",
//        Destinations = new Dictionary<string, DestinationConfig>
//        {
//            ["destination1"] = new(){ Address = "https://localhost:5005"}
//        }
//    },
//    new ClusterConfig()
//    {
//        ClusterId = "API3-Cluster",
//        Destinations = new Dictionary<string, DestinationConfig>
//        {
//            ["destination1"] = new(){ Address = "https://localhost:5006"}
//        }
//    }
// ]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["Jwt:Issuer"],
               ValidAudience = builder.Configuration["Jwt:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
           };
       });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
