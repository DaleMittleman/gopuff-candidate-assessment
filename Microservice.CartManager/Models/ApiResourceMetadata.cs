namespace Microservice.CartManager.Models
{
    using System;

    /// <summary>
    /// Resource metadata.
    /// </summary>
    public class ApiResourceMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResourceMetadata"/> class.
        /// </summary>
        public ApiResourceMetadata()
        {
            var now = DateTime.Now;

            this.Created = now;
            this.LastModified = now;
        }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Gets or sets the resource location.
        /// </summary>
        public string Location { get; set; }
    }
}
