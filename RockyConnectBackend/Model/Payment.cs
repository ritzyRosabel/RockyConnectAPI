using System;
namespace RockyConnectBackend.Model
{

    public class Payment
    {
        public string ID { get; set; }
        public  string DriOwnEmail { get; set; }
        public  string RidRentEmail { get; set; }
        public string? PaymentType { get; set; }
        public  string Bill { get; set; }
        public string? PaymentStatus { get; set; }
        public  string TripID { get; set; }
        public DateTime PaymentDate { get; set; }

    }
    public class PaymentRequest
    {
        public required string DrivOwnEmail { get; set; }
        public required string RidRentEmail { get; set; }
        public PaymentCard? Card { get; set; }
        public required string Bill { get; set; }
        public string? CardAlias { get; set; }
        public required string TripID { get; set; }

    }

    public class PaymentCard
    {
        public string? Email { get; set; }
        public string? CardAlias { get; set; }
        public string? CardType { get; set; }
        public string? Pan { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }

    }
    public class PaymentCardRequest
    {
        public string? Email { get; set; }
        public string? CardAlias { get; set; }
        public string? CardType { get; set; }
        public string? Pan { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
    public class SavedCardsRequest
    {
        public required string Email { get; set; }

    }
    public class SavedCardRequest
    {
        public string? Email { get; set; }
        public string? CardAlias { get; set; }

    }

    public class CardUpdate
    {
        public required string Email { get; set; }
        public required string CardAlias { get; set; }
        public required string OldCardAlias { get; set; }

    }
    public class Transaction
    {
        public int ID { get; set; }
        public User? Driver { get; set; }
        public User? Rider { get; set; }
        public required string bill { get; set; }
        public required string tripID { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public required string PaymentStatus { get; set; }
    }
    public class RefundRequest
    {
        public PaymentMethod RefundType { get; set; }
        public required string DrivOwnEmail { get; set; }
        public required string RidRentEmail { get; set; }
        public required string Bill { get; set; }
        public required string PaymentID { get; set; }
    }

    public class Refund
    {
        public string? Driver { get; set; }
        public string? Rider { get; set; }
        public required string Bill { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public DateTime RefundDate { get; set; }
        public required string RefundStatus { get; set; }
        public required string TransactionID { get; set; }
    }


    public enum PaymentMethod
    {
        card = 1,
        cash = 2,

    }
}