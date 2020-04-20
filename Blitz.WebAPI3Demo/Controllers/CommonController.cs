using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Blitz.WebAPI3Demo.Controllers
{
    /// <summary>
    /// Common Controller
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "common")]
    public class CommonController : ControllerBase
    {
        private readonly ILogger _logger;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="logger">ILogger</param>
        public CommonController(ILogger<CommonController> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Version Information
        /// </summary>
        /// <returns>(sic)</returns>
        [HttpGet("/version")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Version()
        {
            return this.Ok(Program.ProgramMetadata);
        }

        /// <summary>
        /// Health Check
        /// </summary>
        /// <returns></returns>
        [HttpGet("/health")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ObjectResult HealthCheck()
        {
            var info = new Dictionary<string, object>()
            {
                { "Product", Program.ProgramMetadata.Product },
                { "Copyright", Program.ProgramMetadata.Copyright },
                { "Company", Program.ProgramMetadata.Company },
                { "SemanticVersion", Program.ProgramMetadata.SemanticVersion },
                { "TeamName", Program.ProgramMetadata.TeamName },
                { "TeamEmail", Program.ProgramMetadata.TeamEmail },
                { "GitHubUrl", Program.ProgramMetadata.GitHubUrl },
            };

            var deps = new Dictionary<string, object>()
            {
                { "None", "Ok" }
            };

            var data = new Dictionary<string, object>()
            {
                { "Information", info },
                { "Dependencies", deps },
            };


            Exception ex = null;

            var hcr = (ex == null) ?
                new HealthCheckResult(HealthStatus.Healthy, $"{Program.ProgramMetadata.Product}", ex, data) :
                new HealthCheckResult(HealthStatus.Unhealthy, $"{Program.ProgramMetadata.Product}", ex, data);

            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(hcr);

            int statusCode = 200;
            if(ex == null)
            {
                this._logger.LogDebug(msg);
            } else
            {
                this._logger.LogError(msg);
                statusCode = 500;
            }

            return this.StatusCode(statusCode, hcr);
        }

    }
}