using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcelotGateway.Common;

namespace OcelotGateway.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration) : ControllerBase
    {
        // POST: api/auth
        [HttpPost]
        public async Task<IActionResult> GetAccessToken([FromBody] TokenRequestModel model)
        {
            if (!IsModelCorrect(model))
            {
                return StatusCode(400, "Model is incorrect!");
            }

            try
            {
                // Create an instance of HttpClient using the factory
                var client = httpClientFactory.CreateClient();

                // Specify the token endpoint URL
                var tokenEndpoint = configuration["KeycloakTokenUrl"];

                // Prepare the request content (form-urlencoded) with required parameters
                var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "cartservice"),
                    new KeyValuePair<string, string>("client_secret", "cartservice"),
                    new KeyValuePair<string, string>("username", model.Username),
                    new KeyValuePair<string, string>("password", model.Password)
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

        private static bool IsModelCorrect(TokenRequestModel model)
            => !string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrWhiteSpace(model.Username);
    }
}
