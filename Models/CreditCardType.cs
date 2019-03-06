using System;
using System.Collections.Generic;

namespace CreditCardGenerator.Models
{
    public class CreditCardType
    {
        public int Id {get; set;}
        public string Description {get;set;}
        public int CardNumberLength {get; set;}
        public string[] CardNumberPrefixes {get;set;} 
        public bool ValidateWithLuhnAlgorithm {get; set;}
        public string CustomPrefix {get;set;}
    }
}