namespace Microservice.CartManager.Models
{
    /// <summary>
    /// Defines an set of image URIs.
    /// </summary>
    public class ImageSet
    {
        /// <summary>
        /// Gets or sets the URI for the small image.
        /// </summary>
        public string Small { get; set; }

        /// <summary>
        /// Gets or sets the URI for the thumbnail image.
        /// </summary>
        public string Thumb { get; set; }

        /// <summary>
        /// Gets or sets the URI for the medium image.
        /// </summary>
        public string Medium { get; set; }

        /// <summary>
        /// Gets or sets the URI for the extra large image.
        /// </summary>
        public string Xlarge { get; set; }
    }
}