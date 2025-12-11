namespace Ecommerce.Helpers
{
    public enum PaymentStatus
    {
        Pending,          // Payment is initiated but not completed
        Completed,        // Payment was successfully processed
        Failed,           // Payment attempt failed
        Refunded,         // Payment was refunded to the customer
        Cancelled,        // Payment was cancelled before processing

    }
}