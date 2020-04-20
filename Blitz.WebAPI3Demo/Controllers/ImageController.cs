using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blitz.WebAPI3Demo.Controllers
{
    /// <summary>
    /// Image Method Demos
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "images")]
    [Route("/Images")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostingEnv = null;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="env">IHostingEnvironment</param>
        public ImageController(ILogger<ImageController> logger, IWebHostEnvironment env)
        {
            this._logger = logger;
            this._hostingEnv = env;
        }

        /// <summary>
        /// Get Logo
        /// </summary>
        /// <returns>Logo as Png</returns>
        [HttpGet("Logo")]
        [Produces("image/png")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLogo()
        {
            var filename = "blizkrieglogo300.png";
            var path = System.IO.Path.Combine(this._hostingEnv.ContentRootPath, "assets", "images", filename);
            
            this._logger.LogDebug($"GetLogo({path})");

            var b = System.IO.File.ReadAllBytes(path);
            return this.File(b, "image/png", filename);
        }
    }
}