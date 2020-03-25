namespace Microservice.CartManager.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Model for a goPuff product.
    /// </summary>
    public class Product : ApiResource
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the puff price.
        /// </summary>
        public decimal PuffPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is available for bonus.
        /// </summary>
        public bool AvailableForBonus { get; set; }

        /// <summary>
        /// Gets or sets an offer description.
        /// </summary>
        public string Offer { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the subcategory id.
        /// </summary>
        public int SubcategoryId { get; set; }

        /// <summary>
        /// Gets or sets the subcategory.
        /// </summary>
        public string Subcategory { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// Gets or sets the class of product.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the product family.
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets the brand's parent company.
        /// </summary>
        public string ParentCompany { get; set; }

        /// <summary>
        /// Gets or sets the brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the size of the item.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Gets or sets the image URIs.
        /// </summary>
        public IEnumerable<ImageSet> Images { get; set; }

        /// <summary>
        /// Gets or sets the kind of item.
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is a tabacco product.
        /// </summary>
        public int IsTobacco { get; set; }

        /// <summary>
        /// Gets or sets UPC values.
        /// </summary>
        public IEnumerable<string> Upc { get; set; }

        /// <summary>
        /// Gets or sets the avatar image URIs.
        /// </summary>
        public ImageSet Avatar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the product is discontinued.
        /// </summary>
        public int IsDiscontinued { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the product is hidden from user searches.
        /// </summary>
        public int IsHiddenFromSearch { get; set; }

        /// <summary>
        /// Gets or sets the number of product orders that have been confirmed.
        /// </summary>
        public int Confirmed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is part of a bundle.
        /// </summary>
        public bool Bundle { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the badge details.
        /// </summary>
        [JsonPropertyName("badgeDetails")]
        public IEnumerable<string> BadgeDetails { get; set; }

        /// <summary>
        /// Gets or sets the badges.
        /// </summary>
        public IEnumerable<string> Badges { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }
    }
}