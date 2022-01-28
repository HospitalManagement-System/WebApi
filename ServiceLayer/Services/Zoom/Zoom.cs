using ServiceLayer.Interfaces.IZoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;



namespace ServiceLayer.Services.Zoom
{
    public class Zoom : IZoom
    {
        Tuple<string, string> IZoom.Zoom()
        {

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var apiSecret = "wAcHGaptiIESm478FDJ2DeAbhDQiitIpI9yT";
            byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "JKnOKOhMRMqbrE_01tvCag",
                Expires = now.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var client = new RestClient("https://api.zoom.us/v2/users/info@techussain.com/meetings");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { topic = "Meeting", duration = "10", start_time = "2021-04-20T05:00:00", type = "2" });

            request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
            IRestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            int numericStatusCode = (int)statusCode;
            var jObject = JObject.Parse(restResponse.Content);

            var start = (string)jObject["start_url"];
            var Join = (string)jObject["join_url"];
            var Text1 = Convert.ToString(numericStatusCode);

            return new Tuple<string, string>(start, Join);
        }
    }
}
