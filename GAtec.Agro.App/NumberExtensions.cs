using System;
using System.Globalization;

namespace GAtec.Agro.App
{
    public static class NumberExtensions
    {

        public static string FormatCurrency(this decimal value, string cultureName)
        {
            var culture = new CultureInfo(cultureName);

            return value.ToString("C2", culture);
        }

        public static string FormatCurrencyBR(this decimal value)
        {
            var culture = new CultureInfo("pt-BR");

            return value.ToString("C2", culture);
        }

        public static string FormatCurrencyUS(this decimal value)
        {
            var culture = new CultureInfo("en-US");

            return value.ToString("C2", culture);
        }

        public static string FormatCurrency(this Produto value)
        {
            return value.Preco.FormatCurrencyBR();
        }

        public static void Print(this object value)
        {
            Console.WriteLine(value);
        }

    }
}
