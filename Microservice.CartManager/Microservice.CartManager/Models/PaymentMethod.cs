namespace Microservice.CartManager.Models
{
    /// <summary>
    /// An enumeration of potential payment methods.
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Paid with a credit / debit card.
        /// </summary>
        CreditCard,

        /// <summary>
        /// Paid with the debit from a gift card.
        /// </summary>
        GiftCard,

        /// <summary>
        /// Paid using PayPal.
        /// </summary>
        PayPal,

        /// <summary>
        /// Paid using a digital wallet (eg Apple Pay).
        /// </summary>
        DigitalWallet,
    }
}