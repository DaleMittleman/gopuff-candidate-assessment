namespace Microservice.CartManager.Models
{
    /// <summary>
    /// Container for API resource shared properties.
    /// </summary>
    public abstract class ApiResource
    {
        /// <summary>
        /// Gets or sets metadata about the resource being accessed.
        /// </summary>
        public ApiResourceMetadata Meta { get; set; }
    }
}
