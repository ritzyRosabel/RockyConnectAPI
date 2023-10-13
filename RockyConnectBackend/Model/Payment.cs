using System;
namespace RockyConnectBackend.Model
{
	public class PaymentRequest
	{
		public string DrivOwnEmail { get; set; }
		public string RidRentEmail { get; set; }
		public PaymentCard card { get; set; }
		public string bill { get; set; }
		public string tripID { get; set; }

	}

	public class PaymentCard
	{
		public string Email { get; set; }
		public string CardAlias  { get; set; }
		public string CardType { get; set; }
		public string Code { get; set; }
		public string FullName { get; set; }
		public DateTime ExpiryDate { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }

    }
    public class CardUpdate
    {
        public string Email { get; set; }
        public string CardAlias { get; set; }
        public string OldCardAlias { get; set; }
        public string CardType { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }

    }
    public class Transaction
	{
		public int ID { get; set; }
		public User Driver { get; set; }
		public User Rider { get; set; }
		public string bill { get; set; }
		public string tripID { get; set; }
		public PaymentMethod paymentMethod { get; set; }
		public DateTime PaymentDate { get; set; }
		public string PaymentStatus { get; set; }
		public object MyProperty { get; set; }
	}
	public class RefundRequest
	{
		public PaymentMethod RefundType { get; set; }
		public string DrivOwnEmail { get; set; }
        public string RidRentEmail { get; set; }
        public PaymentCard card { get; set; }
        public string bill { get; set; }
        public string tripID { get; set; }
    }

	public class Refund
	{
        public User Driver { get; set; }
        public User Rider { get; set; }
        public string bill { get; set; }
        public string tripID { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public DateTime RefundDate { get; set; }
        public string RefundStatus { get; set; }
        public object TransactionID { get; set; }
    }

	
	public enum PaymentMethod
	{
		card =1,
		cash =2,

	}
}