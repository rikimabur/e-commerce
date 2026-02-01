namespace Payments.Domain.Enums;
public enum PaymentStatus
{
    Pending = 0,     // Payment created, waiting for bank
    Succeeded = 1,   // Bank confirmed success
    Failed = 2       // Bank confirmed failure
}