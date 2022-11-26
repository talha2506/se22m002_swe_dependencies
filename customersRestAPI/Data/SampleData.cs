using customersRestAPI.Entities;

namespace customersRestAPI.Data
{
    public class SampleData
    {
        // private static readonly Random random = new Random();

        public static readonly FinancialProduct[] financialProducts = new[]
        {
            new FinancialProduct
            {
                Name = "Girokonto",
                Balance = 100000,
                ProductCode = "GK12345789",
                InterestRate = 3
            },
            new FinancialProduct
            {
                Name = "Sparbuch",
                Balance = 3000,
                ProductCode = "SB12384849",
                InterestRate = 1
            },
            new FinancialProduct
            {
                Name = "ETF",
                Balance = 2300,
                ProductCode = "ETF128383",
                InterestRate = 9
            },
            new FinancialProduct
            {
                Name = "BTC",
                Balance = 7000,
                ProductCode = "BITCOINTOTHEMOON",
                InterestRate = -7
            },
            new FinancialProduct
            {
                Name = "Aktiendepot",
                Balance = -23000,
                ProductCode = "TWITTER",
                InterestRate = -1
            }
        };

        // removed this function as it generates a new list of financialProducts every time, the calls are executed ...
        /*public static List<FinancialProduct> GetRandomListOfFinancialProducts()
        {
            return financialProducts.OrderBy(product => random.Next()).Take(random.Next(6)).ToList();
        }*/

        public static readonly Customer[] customers = new[]
        {
            new Customer {
                Name = "Max Mustermann",
                Address = "Regenstraße 1, 1030 Wien",
                Birthdate = DateTime.Now.AddYears(-17),
                EmailAddress = "max.mustermann@gmx.at",
                Status = "active"
            },
            new Customer {
                Name = "Maxime Musterfrau",
                Address = "Regenstraße 1, 1030 Wien",
                Birthdate = DateTime.Now.AddYears(-21),
                EmailAddress = "maxime.musterfrau@gmx.at",
                Status = "active"
            },
            new Customer {
                Name = "Max Mustervater",
                Address = "Regenstraße 1, 1030 Wien",
                Birthdate = DateTime.Now.AddYears(-34),
                EmailAddress = "max.mustervater@gmx.at",
                Status = "active"
            },
            new Customer {
                Name = "Maxime Mustermama",
                Address = "Regenstraße 1, 1030 Wien",
                Birthdate = DateTime.Now.AddYears(-32),
                EmailAddress = "maxime.mustermama@gmx.at",
                Status = "active"
            },
            new Customer {
                Name = "Max Musteropa",
                Address = "Regenstraße 1, 1030 Wien",
                Birthdate = DateTime.Now.AddYears(-65),
                EmailAddress = "max.musteropa@gmx.at",
                Status = "active"
            }
        };
    }
}
