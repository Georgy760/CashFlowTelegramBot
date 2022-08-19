using System.Globalization;
using System.Xml;

namespace CashFlowTelegramBot.Skywards;

internal class CurrencyConverter
{
    /// <summary>
    ///     Gets all available currency tags
    /// </summary>
    public static string[] GetCurrencyTags()
    {
        // Hardcoded currency tags neccesairy to parse the ecb xml's
        return new[]
        {
            "eur", "usd", "jpy", "bgn", "czk", "dkk", "gbp", "huf", "ltl", "lvl", "pln", "ron", "sek", "chf", "nok",
            "hrk", "rub", "try", "aud", "brl", "cad", "cny", "hkd", "idr", "ils", "inr", "krw", "mxn", "myr", "nzd",
            "php", "sgd", "zar"
        };
    }

    /// <summary>
    ///     Get currency exchange rate in euro's
    /// </summary>
    public static float GetCurrencyRateInEuro(string currency)
    {
        //if (currency.ToLower() == "")
        throw new ArgumentException("Invalid Argument! currency parameter cannot be empty!");
        if (currency.ToLower() == "eur")
            throw new ArgumentException("Invalid Argument! Cannot get exchange rate from EURO to EURO");

        try
        {
            // Create with currency parameter, a valid RSS url to ECB euro exchange rate feed
            var rssUrl = string.Concat("http://www.ecb.int/rss/fxref-", currency.ToLower() + ".html");

            // Create & Load New Xml Document
            var doc = new XmlDocument();
            doc.Load(rssUrl);

            // Create XmlNamespaceManager for handling XML namespaces.
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
            nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");

            // Get list of daily currency exchange rate between selected "currency" and the EURO
            var nodeList = doc.SelectNodes("//rdf:item", nsmgr);


            // Loop Through all XMLNODES with daily exchange rates
            foreach (XmlNode node in nodeList)
            {
                // Create a CultureInfo, this is because EU and USA use different sepperators in float (, or .)
                var ci = (CultureInfo) CultureInfo.CurrentCulture.Clone();
                ci.NumberFormat.CurrencyDecimalSeparator = ".";

                try
                {
                    // Get currency exchange rate with EURO from XMLNODE
                    var exchangeRate = float.Parse(
                        node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr).InnerText,
                        NumberStyles.Any,
                        ci);

                    return exchangeRate;
                }
                catch
                {
                }
            }

            // currency not parsed!! 
            // return default value
            return 0;
        }
        catch
        {
            // currency not parsed!! 
            // return default value
            return 0;
        }
    }

    /// <summary>
    ///     Get The Exchange Rate Between 2 Currencies
    /// </summary>
    public static float GetExchangeRate(string from, string to, float amount = 1)
    {
        // If currency's are empty abort
        if (from == null || to == null)
            return 0;

        // Convert Euro to Euro
        if (from.ToLower() == "eur" && to.ToLower() == "eur")
            return amount;

        try
        {
            // First Get the exchange rate of both currencies in euro
            var toRate = GetCurrencyRateInEuro(to);
            var fromRate = GetCurrencyRateInEuro(from);

            // Convert Between Euro to Other Currency
            if (from.ToLower() == "eur")
                return amount * toRate;
            if (to.ToLower() == "eur")
                return amount / fromRate;
            return amount * toRate / fromRate;
        }
        catch
        {
            return 0;
        }
    }
}