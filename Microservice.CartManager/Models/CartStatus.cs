namespace Microservice.CartManager.Models
{
    /// <summary>
    /// List of valid cart statuses.
    /// </summary>
    public enum CartStatus
    {
        /// <summary>
        /// A new cart, just created.
        /// </summary>
        New,

        /// <summary>
        /// An active cart, with items being added and rmeoved.
        /// </summary>
        Active,

        /// <summary>
        /// A validated cart, staged for checkout.
        /// </summary>
        Validated,

        /// <summary>
        /// A checked out cart, locked and ready for payment.
        /// </summary>
        CheckedOut,

        /// <summary>
        /// An ordered cart, paid and shipped.
        /// </summary>
        Ordered,
    }
}