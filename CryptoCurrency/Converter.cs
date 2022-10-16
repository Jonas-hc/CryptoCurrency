using CryptoCurrency;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CryptoCurrency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            String to = "Bitcoin";
            String from = "SomeCoin";
            double amount = 1;

            Converter converter = new Converter();
            converter.SetPricePerUnit(to, 30000);
            converter.SetPricePerUnit(from, 30);

            double res = converter.Convert(from, to, amount);

            Console.WriteLine(amount + " " + from + " is equal to " + res + " " + to);
        }
    }

    public class Crypto
    {
        public String name { get; set; }
        public double price { get; set; }
        public Crypto(String cryptoName, double cryptoPrice)
        {
            name = cryptoName;
            price = cryptoPrice;
        }
    }
    public class Converter
    {
        public static List<Crypto> cryptoList = new List<Crypto>();
        /// <summary>
        /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
        /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
        /// bliver den gamle værdi overskrevet af den nye værdi
        /// </summary>
        /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
        /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
        public void SetPricePerUnit(String currencyName, double price)
        {
            if (!cryptoList.Any(c => c.name.ToLower() == currencyName.ToLower() ))
            {
                Crypto newCrypto = new Crypto(currencyName, price);
                cryptoList.Add(newCrypto);
                Console.WriteLine("New Crypto added");
            }
        }

        /// <summary>
        /// Konverterer fra en kryptovaluta til en anden. 
        /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
        /// 
        /// </summary>
        /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
        /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
        /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
        /// <returns>Værdien af beløbet i toCurrencyName</returns>
        public double Convert(String fromCurrencyName, String toCurrencyName, double amount) 
        {
            Crypto fromCrypto;
            Crypto toCrypto;

            if (cryptoList.Any(c => c.name.ToLower() == fromCurrencyName.ToLower()))
            {
                fromCrypto = cryptoList.Find(c => c.name.ToLower() == fromCurrencyName.ToLower());
            } 
            else
            {
                throw new ArgumentException(fromCurrencyName + " does not exsists");
            }

            if (cryptoList.Any(c => c.name.ToLower() == toCurrencyName.ToLower()))
            {
                toCrypto = cryptoList.Find(c => c.name.ToLower() == toCurrencyName.ToLower());
            }
            else
            {
                throw new ArgumentException(toCurrencyName + " does not exsists");
            }

            double konverted = amount * (fromCrypto.price / toCrypto.price);

            return konverted;
        }
    }
}
