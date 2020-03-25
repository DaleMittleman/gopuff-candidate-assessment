namespace Microservice.CartManager.Models
{
    /// <summary>
    /// Payment info.
    /// </summary>
    public class PaymentInformation
    {
        /// <summary>
        /// Gets or sets the payment method.
        /// </summary>
        public PaymentMethod? PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        public string BillingAddress { get; set; }
    }
}
