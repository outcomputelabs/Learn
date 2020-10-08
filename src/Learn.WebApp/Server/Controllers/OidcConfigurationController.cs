using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Learn.WebApp.Server.Controllers
{
    [ApiController]
    [Route("_configuration")]
    public class OidcConfigurationController : Controller
    {
        private readonly IClientRequestParametersProvider _provider;

        public OidcConfigurationController(IClientRequestParametersProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        [HttpGet("{clientId}")]
        public ActionResult<IDictionary<string, string>> GetClientRequestParameters([FromRoute] string clientId)
        {
            IDictionary<string, string> parameters;
            try
            {
                parameters = _provider.GetClientParameters(HttpContext, clientId);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }

            return Ok(parameters);
        }
    }
}