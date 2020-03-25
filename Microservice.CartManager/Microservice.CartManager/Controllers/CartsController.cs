namespace Microservice.CartManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Exposes operations on the Carts collection.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CartsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartsController"/> class.
        /// </summary>
        /// <param name="logger">A logger for this controller.</param>
        public CartsController(ILogger<CartsController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get all carts.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> that wraps the Cart content.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            this.logger.Log(LogLevel.Information, "Called: GET /Carts");

            return this.Ok();
        }
    }
}
