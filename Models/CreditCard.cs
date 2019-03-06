using System;

namespace CreditCardGenerator.Models
{
    public class CreditCard
    {
        public CreditCardType Type {get; set;}        
        public string Number {get; set;}
        public string Cvv {get; set;}
        public DateTime ExpirationDate {get; set;}
    }
}