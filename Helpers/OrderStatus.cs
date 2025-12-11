namespace Ecommerce.Helpers
{
    public enum OrderStatus
    {
        Pending,          // Order has been created but not yet confirmed
        Confirmed,        // Order has been confirmed
        Processing,       // Order is being processed
        Shipped,          // Order has been shipped
        Delivered,        // Order has been delivered to the customer
        Cancelled,        // Order was cancelled by the customer or admin
        Returned,         // Order was returned by the customer
        Failed            // Order processing failed
    }
}