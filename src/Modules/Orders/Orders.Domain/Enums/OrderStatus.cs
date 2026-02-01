namespace Orders.Domain.Enums;
public enum OrderStatus
{
    Created = 0,        // Order placed
    Paid = 1,           // Payment succeeded
    PaymentFailed = 2   // Payment failed
}
