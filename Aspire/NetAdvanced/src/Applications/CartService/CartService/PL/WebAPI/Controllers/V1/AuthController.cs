using Asp.Versioning;
using CartService.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartService.PL.WebAPI.Controllers.V1
{
    // This controller should be in KeyCloak, but there is no project for KeyCloak since I'm using Aspire
    // Maybe I should move it to ApiGateway then
    [ApiController, ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/auth")]
    [AllowAnonymous]
    public class AuthController(IHttpClientFactory httpClientFactory) : ControllerBase
    {
        // POST: api/v{version}/auth
        [HttpPost]
        public async Task<IActionResult> GetAccessToken([FromBody] TokenRequestModel model)
        {
            try
            {
                // Create an instance of HttpClient using the factory
                var client = httpClientFactory.CreateClient();

                // TODO: Update it to take such values from Configuration
                // Specify the token endpoint URL
                var tokenEndpoint = "http://localhost:8080/realms/master/protocol/openid-connect/token";

                // Prepare the request content (form-urlencoded) with required parameters
                var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "cartservice"),
                    new KeyValuePair<string, string>("client_secret", "cartservice"),
                    new KeyValuePair<string, string>("username", model.Username ?? ""),
                    new KeyValuePair<string, string>("password", model.Password ?? "")
                });

                // Send a POST request to the token endpoint with the prepared request content
                var response = await client.PostAsync(tokenEndpoint, requestContent);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the access token from the response content
                    var accessToken = await response.Content.ReadAsStringAsync();
                    return Ok(accessToken); // Return the access token
                }
                else
                {
                    // Return an appropriate status code with a message indicating failure
                    return StatusCode((int)response.StatusCode, $"Failed to get access token. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error with details of the exception
                return StatusCode(500, $"An error occurred while retrieving access token: {ex.Message}");
            }
        }
    }
}
