using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public string CardHolderName { get; private set; }
        public string CardNumber { get;private  set; }
        public string LastTransactionNumber { get; private set; }
        

        public CreditCardPayment(DateTime paidDate, 
                                 DateTime expireDate, 
                                 decimal total, 
                                 decimal totalPaid, 
                                 string payer,
                                 Document document,
                                 Email email, 
                                 Address address,
                                 string lastTransactionNumber) : 
                                 base(paidDate, expireDate, total, totalPaid,payer, document, email, address)
        {
            LastTransactionNumber = lastTransactionNumber;
        }
    }
}