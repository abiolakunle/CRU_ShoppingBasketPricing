# ShoppingBasketPricing Documentation

## Overview
This repository contains a C# solution for calculating shopping basket prices, including sub-total, discounts, and total, based on various offer types. The solution is structured into a main library (`ShoppingBasketPricing`) and a test project (`ShoppingBasketPricing.Test`) to ensure functionality and coverage.

## Prerequisites
- **Tools**: 
  - `dotnet` CLI for building and running tests.
  - `reportgenerator` for generating coverage reports (install via `dotnet tool install -g dotnet-reportgenerator-globaltool`).

## Project Structure
- **`ShoppingBasketPricing/`**:
  - **`Items/`**: Contains `BasketItem.cs` and `CatalogueItem.cs` for basket and catalogue item definitions.
  - **`Offers/`**: Includes offer implementations:
    - `GroupBuyKGetCheapestFreeOffer.cs`
    - `IOffer.cs` (interface)
    - `PercentageDiscountOffer.cs`
    - `SingleProductBuyKGetYFreeOffer.cs`
  - **`BasketPricer.cs`**: Core pricing logic.
  - **`ShoppingBasketPricing.csproj`**: Main project file.
- **`ShoppingBasketPricing.Console/`**: Console application for demo purposes.
  - **`Program.cs`**
  - **`ShoppingBasketPricing.Console.csproj`**
- **`ShoppingBasketPricing.Test/`**: Unit tests.
  - **`BasketPricerTests.cs`**
  - **`ShoppingBasketPricing.Test.csproj`**
- **`coverage-report/`**: Directory for generated coverage reports (created after running tests).


## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/ShoppingBasketPricing.git
   cd ShoppingBasketPricing
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Install `reportgenerator` globally (if not already installed):
   ```bash
   dotnet tool install -g dotnet-reportgenerator-globaltool
   ```

## Running the Application
1. Build the solution:
   ```bash
   dotnet build
   ```
2. Run the console application to see a demo:
   ```bash
   cd ShoppingBasketPricing.Console
   dotnet run
   ```
   - The console app demonstrates pricing calculations with sample data.

## Running Tests
1. Navigate to the test project directory:
   ```bash
   cd ShoppingBasketPricing.Test
   ```
2. Run tests with coverage collection:
   ```bash
   dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
   ```
   - This generates `coverage.opencover.xml` in the test project directory.
3. Generate a coverage report:
   ```bash
   reportgenerator -reports:coverage.opencover.xml -targetdir:../coverage-report -reporttypes:Html
   ```
   - Open `coverage-report/index.html` in a web browser to view the coverage report.
4. Verify test results and coverage metrics.

## Key Features
- **Pricing Calculation**: `BasketPricer.CalculatePrices` computes sub-total, discount, and total with rounding to two decimal places.
- **Offer Types**:
  - `PercentageDiscountOffer`: Applies a percentage discount to matching items.
  - `SingleProductBuyKGetYFreeOffer`: Buy K items, get Y free of the same product.
  - `GroupBuyKGetCheapestFreeOffer`: Buy K items across a group, get the cheapest free.
