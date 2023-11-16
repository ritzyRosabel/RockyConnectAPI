using System;
namespace RockyConnectBackend.Model
{

    public class Payment
    {
        public string ID { get; set; }
        public  string DriOwnEmail { get; set; }
        public  string RidRentEmail { get; set; }
        public string? PaymentType { get; set; }
        public  int Bill { get; set; }
        public string? PaymentStatus { get; set; }
        public  string TripID { get; set; }
        public string RefundID { get; set; }
        public DateTime PaymentDate { get; set; }

    }
    /// <summary>This is Payment Endpoints</summary>
    public class PaymentRequest
    {
        public required string DrivOwnEmail { get; set; }
        public required string RidRentEmail { get; set; }
        public PayCard? Card { get; set; }
        public required int Bill { get; set; }
        public string? CardAlias { get; set; }
        public required string TripID { get; set; }
        public bool SavedCard { get; set; }

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
    /// <summary>This is Payment Endpoints</summary>
    public class PaymentCardRequest
    {
        public string? Email { get; set; }
        public string? CardAlias { get; set; }
        public string? CardType { get; set; }
        public string? Pan { get; set; }
        public string? FullName { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
    public class PayCard
    {
        public string? CardType { get; set; }
        public string? Pan { get; set; }
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


    public class Refund
    {
        public string? ID { get; set; }
        public string? PaymentID { get; set; }
        public int? Bill { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime RefundDate { get; set; }
        public required string RefundStatus { get; set; }
    }

    public class Bank{

        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public string BankName { get; set; }
}
    
}