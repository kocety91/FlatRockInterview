using System.Text;
using HtmlAgilityPack;
using System.Net;
using FlatRockInterview;
using Newtonsoft.Json;

StringBuilder stringBuilder = new StringBuilder();
stringBuilder
    .GetPdfData()
    .FilterPdfData();

HtmlDocument doc = new HtmlDocument();
doc.LoadHtml(stringBuilder.ToString());
var productsInfo = new List<ProductInfo>();
HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='item']");

foreach (var node in nodes)
{
    var productInfo = ExtractProductInfo(node);

    productsInfo.Add(productInfo);
}


var serializeData = JsonConvert.SerializeObject(productsInfo, Formatting.Indented);

Console.WriteLine(serializeData);

static ProductInfo ExtractProductInfo(HtmlNode node)
{
    //Product Names
    var imgNode = node.SelectSingleNode(".//figure//a/img");
    var encodedAltValue = imgNode.GetAttributeValue("alt", string.Empty);
    var decodedName = WebUtility.HtmlDecode(encodedAltValue);

    //Product price
    var priceNode = node.SelectSingleNode
        (".//div[contains(@class,'item-detail')]//div[contains(@class,'pricing')]//p[contains(@class,'price')]/span[contains(@class,'price-display')]");
    var priceSpan = priceNode.SelectSingleNode("./span");
    var price = priceSpan.InnerText.Trim().Replace("$", "");

    //Product Rating
    var ratingAsString = node.GetAttributeValue("rating", string.Empty);
    if (decimal.TryParse(ratingAsString, out decimal rating))
    {
        if (rating > 5)
        {
            rating = rating * 5 / 10;
            Math.Round(rating, 1);
        }
    }

    return new ProductInfo(decodedName, price, rating);
}