
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreditCardGenerator.Enums;
using CreditCardGenerator.Models;
using CreditCardGenerator.Extensions;

namespace CreditCardGenerator.Services
{
    public class CreditCardService
    {
        private Random _random = new Random();
        private const int _minCustomPrefix = 0;
        private const int _maxCustomPrefix = 9;
        private const int _minCvv = 1;
        private const int _maxCvv = 3;
        private const int _minRandomNumber = 0;
        private const int _maxRandomNumber = 9;
        private readonly DateTime _minExpirationDate = DateTime.Today;
        private readonly DateTime _maxExpirationDate = new DateTime(2050, 12, 30);
        public CreditCardType GetCreditCardTypeById(int id)
        {

            List<CreditCardType> creditCardTypes = this.GetCreditCardsTypeMock();

            CreditCardType creditCardType = creditCardTypes.Where(x => x.Id == id).FirstOrDefault();

            return creditCardType;
        }

        public CreditCard GenerateCreditCard(CreditCardType creditCardTypeSelected)
        {

            CreditCard creditCard = new CreditCard();
            bool customPrefixIsValid = this.ValidateCustomPrefix(creditCardTypeSelected.CustomPrefix);
            string prefixSelected = string.Empty;

            if (customPrefixIsValid)
            {
                prefixSelected = creditCardTypeSelected.CustomPrefix;
            }
            else
            {
                string[] prefixes = creditCardTypeSelected.CardNumberPrefixes;

                int randomPrefixIndex = _random.Next(0, prefixes.Length - 1);
                prefixSelected = prefixes[randomPrefixIndex];
            }

            int cardNumberLength = creditCardTypeSelected.CardNumberLength;




            if (prefixSelected.Length < cardNumberLength)
            {

                if (creditCardTypeSelected.ValidateWithLuhnAlgorithm)
                {
                    creditCard.Number = this.GenerateCardNumberByLuhnAlgorithm(prefixSelected, cardNumberLength);
                }
                else
                {
                    creditCard.Number = this.GenerateRandomCardNumber(prefixSelected, cardNumberLength);
                }
            }
            else
            {
                creditCard.Number = prefixSelected;
            }
            creditCard.Cvv = this.GetRandomCvv();
            creditCard.ExpirationDate = this.GetRandomExpirationDate();

            return creditCard;
        }

        #region Private
        private DateTime GetRandomExpirationDate()
        {
            DateTime expirationDate = _random.NextDateTime(_minExpirationDate, _maxExpirationDate);

            return expirationDate;
        }
        private string GetRandomCvv()
        {

            StringBuilder cvvBuilder = new StringBuilder();
            string cvv = string.Empty;
            for (int i = _minCvv; i <= _maxCvv; i++)
            {
                int randomNumber = _random.Next(_minRandomNumber, _maxRandomNumber);
                cvvBuilder.Append(randomNumber);

            }
            cvv = cvvBuilder.ToString();
            return cvv;
        }
        private List<CreditCardType> GetCreditCardsTypeMock()
        {
            List<CreditCardType> creditCardTypes = new List<CreditCardType>();

            #region Adding Mastercard
            CreditCardType mastercard = new CreditCardType
            {
                Id = (int)CreditCardTypeEnum.Mastercard,
                Description = "Mastercard",
                CardNumberLength = 16,
                CardNumberPrefixes = new string[] { "5" },
                ValidateWithLuhnAlgorithm = true
            };


            creditCardTypes.Add(mastercard);
            #endregion

            #region Adding Visa
            CreditCardType visa = new CreditCardType
            {
                Id = (int)CreditCardTypeEnum.Visa,
                Description = "Visa",
                CardNumberLength = 16,
                CardNumberPrefixes = new string[] { "4" },
                ValidateWithLuhnAlgorithm = true
            };

            creditCardTypes.Add(visa);
            #endregion

            #region Adding American Express
            CreditCardType amex = new CreditCardType
            {
                Id = (int)CreditCardTypeEnum.Amex,
                Description = "American Express",
                CardNumberLength = 15,
                CardNumberPrefixes = new string[] { "34", "37" },
                ValidateWithLuhnAlgorithm = true
            };

            creditCardTypes.Add(amex);
            #endregion

            #region Adding Cabal
            CreditCardType cabal = new CreditCardType
            {
                Id = (int)CreditCardTypeEnum.Cabal,
                Description = "CABAL",
                CardNumberLength = 16,
                CardNumberPrefixes = new string[] { "604", "589657", "603522" },
                ValidateWithLuhnAlgorithm = false
            };

            creditCardTypes.Add(cabal);
            #endregion

            return creditCardTypes;
        }
        private string GenerateRandomCardNumber(string prefix, int cardLength)
        {
            StringBuilder cardNumberBuilder = new StringBuilder();
            string cardNumber = string.Empty;
            cardNumberBuilder.Append(prefix);
            int numbersToGenerate = cardLength - prefix.Length;
            for (var i = 0; i < numbersToGenerate; i++)
            {
                int randomNumber = _random.Next(_minRandomNumber, _maxRandomNumber);
                cardNumberBuilder.Append(randomNumber);
            }

            cardNumber = cardNumberBuilder.ToString();

            return cardNumber;

        }

        private bool ValidateCustomPrefix(string customPrefix)
        {

            var isNumeric = int.TryParse(customPrefix, out int n);
            bool isValid = isNumeric && (customPrefix != string.Empty) && customPrefix.Length > _minCustomPrefix
            && customPrefix.Length <= _maxCustomPrefix;

            return isValid;
        }
        private string GenerateCardNumberByLuhnAlgorithm(string prefix, int cardLength)
        {

            StringBuilder cardNumberBuilder = new StringBuilder();
            string cardNumber = string.Empty;

            cardNumberBuilder.Append(prefix);

            this.FillCardNumber(cardNumberBuilder, cardLength);
            this.CalculateLastDigitVerifier(cardNumberBuilder, cardLength);

            cardNumber = cardNumberBuilder.ToString();
            return cardNumber;


        }
        private void CalculateLastDigitVerifier(StringBuilder cardNumber, int cardLength)
        {
            int offset = (cardLength + 1) % 2;
            int finalCardNumber = 0;
            int sum = 0;
            int finalDigit = 0;

            for (int position = 0; position < cardLength; position++)
            {
                if ((position + offset) % 2 == 0)
                {
                    finalCardNumber = Convert.ToInt32(cardNumber[position]) * 2;
                    if (finalCardNumber > 9)
                    {
                        finalCardNumber -= 9;
                    }
                    sum += finalCardNumber;
                }
                else
                {
                    sum += Convert.ToInt32(cardNumber[position]);
                }
            }
            finalDigit = (10 - (sum % 10)) % 10;
            cardNumber[cardLength - 1] = Convert.ToChar(finalDigit.ToString());
        }
        private void FillCardNumber(StringBuilder cardNumber, int cardLength)
        {

            while (cardNumber.Length < cardLength)
            {
                int randomNumber = _random.Next(_minRandomNumber, _maxRandomNumber);
                cardNumber = cardNumber.Append(randomNumber);
            }
        }

        #endregion
    }
}