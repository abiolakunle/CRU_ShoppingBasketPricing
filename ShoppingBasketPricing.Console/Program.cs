using ShoppingBasketPricing.Items;
using ShoppingBasketPricing.Offers;
using ShoppingBasketPricing;

var bakedBeans = new CatalogueItem("Baked Beans", 0.99m);
var biscuits = new CatalogueItem("Biscuits", 1.20m);
var sardines = new CatalogueItem("Sardines", 1.89m);
var shampooSmall = new CatalogueItem("Shampoo (Small)", 2.00m);
var shampooMedium = new CatalogueItem("Shampoo (Medium)", 2.50m);
var shampooLarge = new CatalogueItem("Shampoo (Large)", 3.50m);

// Define offers
var offers = new List<IOffer>
        {
            new SingleProductBuyKGetYFreeOffer(bakedBeans.Id, 2, 1), // Buy 2 get 1 free on Baked Beans
            new PercentageDiscountOffer(new List<Guid> { sardines.Id }, 25), // 25% off Sardines
            new GroupBuyKGetCheapestFreeOffer(new List<Guid> { shampooSmall.Id, shampooMedium.Id, shampooLarge.Id }, 2) // Buy 2 get cheapest free on Shampoos
        };

var pricer = new BasketPricer();

// Scenario 1: Basket with Baked Beans x4, Biscuits x1
var basket1 = new List<BasketItem>
        {
            new BasketItem (bakedBeans, 4),
            new BasketItem (biscuits, 1)
        };
var result1 = pricer.CalculatePrices(basket1, offers);
Console.WriteLine("Scenario 1:");
Console.WriteLine($"Sub-total: £{result1.subTotal:F2}");
Console.WriteLine($"Discount: £{result1.discount:F2}");
Console.WriteLine($"Total: £{result1.total:F2}");

// Scenario 2: Basket with Sardines x2
var basket2 = new List<BasketItem>
        {
            new BasketItem(sardines, 2)
        };
var result2 = pricer.CalculatePrices(basket2, offers);
Console.WriteLine("\nScenario 2:");
Console.WriteLine($"Sub-total: £{result2.subTotal:F2}");
Console.WriteLine($"Discount: £{result2.discount:F2}");
Console.WriteLine($"Total: £{result2.total:F2}");

// Scenario 3: Basket with Shampoo (Large) x3, Shampoo (Medium) x1, Shampoo (Small) x2
var basket3 = new List<BasketItem>
{
    new BasketItem(shampooLarge, 3),
    new BasketItem(shampooMedium, 1),
    new BasketItem(shampooSmall, 2)
};
var result3 = pricer.CalculatePrices(basket3, offers);
Console.WriteLine("\nScenario 3:");
Console.WriteLine($"Sub-total: £{result3.subTotal:F2}");
Console.WriteLine($"Discount: £{result3.discount:F2}");
Console.WriteLine($"Total: £{result3.total:F2}");

// Scenario 4: Empty basket
var basket4 = new List<BasketItem>();
var result4 = pricer.CalculatePrices(basket4, offers);
Console.WriteLine("\nScenario 4:");
Console.WriteLine($"Sub-total: £{result4.subTotal:F2}");
Console.WriteLine($"Discount: £{result4.discount:F2}");
Console.WriteLine($"Total: £{result4.total:F2}");

// Scenario 5: Basket with Biscuits x3 (no offers apply)
var basket5 = new List<BasketItem>
        {
            new BasketItem(biscuits, 3)
        };
var result5 = pricer.CalculatePrices(basket5, offers);
Console.WriteLine("\nScenario 5:");
Console.WriteLine($"Sub-total: £{result5.subTotal:F2}");
Console.WriteLine($"Discount: £{result5.discount:F2}");
Console.WriteLine($"Total: £{result5.total:F2}");

// Scenario 6: Basket with Sardines x1 and multiple 50% discounts
var sardinesDiscount50_1 = new PercentageDiscountOffer(new List<Guid> { sardines.Id }, 50);
var sardinesDiscount50_2 = new PercentageDiscountOffer(new List<Guid> { sardines.Id }, 50);
var sardinesDiscount50_3 = new PercentageDiscountOffer(new List<Guid> { sardines.Id }, 50);
var offersForScenario6 = new List<IOffer> { sardinesDiscount50_1, sardinesDiscount50_2, sardinesDiscount50_3 };
var basket6 = new List<BasketItem>
        {
            new BasketItem(sardines, 1)
        };
var result6 = pricer.CalculatePrices(basket6, offersForScenario6);
Console.WriteLine("\nScenario 6:");
Console.WriteLine($"Sub-total: £{result6.subTotal:F2}");
Console.WriteLine($"Discount: £{result6.discount:F2}");
Console.WriteLine($"Total: £{result6.total:F2}");