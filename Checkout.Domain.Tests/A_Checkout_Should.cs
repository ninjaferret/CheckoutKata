using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CheckoutKata.Domain.Tests
{
    [TestFixture]
    public class A_Checkout_Should
    {
        private const string Apple = "A";
        private const string Dates = "D";
        private const string Bananas = "B";
        private const string Cherries = "C";
        private const string HeinzBeans = "HEI-454";

        private const double CostOfApple = 0.50;
        private const double CostOfDate = 0.15;
        private const double CostOfBananas = 0.30;
        private const double CostOfCherries = 0.20;
        private const double CostOfHeinzBeans = 1.49;

        [Test]
        public void Have_A_Total_Of_Zero_If_No_Items_Are_Scanned()
        {
            var checkout = new Checkout();

            Assert.That(checkout.Total, Is.EqualTo(0m));
        }

       
        [TestCase(Apple,CostOfApple)]
        [TestCase(Bananas,CostOfBananas)]
        [TestCase(Cherries,CostOfCherries)]
        [TestCase(Dates,CostOfDate)]
        [TestCase(HeinzBeans, CostOfHeinzBeans)]
        public void Have_A_Total_Of_Value_If_One_Item_Is_Scanned(string item, decimal value)
        {
            var checkout = new Checkout();
            checkout.Scan(item);
            Assert.That(checkout.Total, Is.EqualTo(value));
        }

        [Test]
        public void Have_A_Total_Value_Of_EightyPence_When_A_Apples_And_A_Banana_Are_Scanned()
        {
            var checkout = new Checkout();
            checkout.Scan(Apple);
            checkout.Scan(Bananas);
            Assert.That(checkout.Total, Is.EqualTo(0.80m));
        }

        [Test]
        public void Have_A_Total_Of_1_Pound_And_25p_When_Three_Apples_Are_Bought()
        {
            var checkout = new Checkout();
            checkout.Scan(Apple);
            checkout.Scan(Apple);
            checkout.Scan(Apple);
            Assert.That(checkout.Total, Is.EqualTo(1.25m));
        }

        [Test]
        public void Have_A_Total_Of_1_Pound_And_50p_When_The_Basket_Totals_150_Without_Offers()
        {
            var checkout = new Checkout();
            checkout.Scan(Apple);
            checkout.Scan(Apple);
            checkout.Scan(Bananas);
            checkout.Scan(Cherries);
            Assert.That(checkout.Total, Is.EqualTo(1.50m));
        }
        [Test]
        public void Have_A_Total_Of_45p_When_The_Basket_Contains_2_Bananas()
        {
            var checkout = new Checkout();
            checkout.Scan(Bananas);
            checkout.Scan(Bananas);
            Assert.That(checkout.Total, Is.EqualTo(.45m));
        }

        [Test]
        public void Lets_You_Customise_Your_Offers()
        {
            var offers = new List<Tuple<string, int, decimal>>
                {
                    new Tuple<string, int, decimal>("C", 3, 0.50m),
                    new Tuple<string, int, decimal>("HEI-454", 3, 3.00m)
                };
            Assert.DoesNotThrow(()=> new Checkout(offers));
        }

        [Test]
        public void Have_An_Offer_Of_3_Cherries_For_50p_When_I_Add_Three_Cherries_The_Total_Is_50p()
        {
            var offers = new List<Tuple<string, int, decimal>>
                {
                    new Tuple<string, int, decimal>("C", 3, 0.50m),
                    new Tuple<string, int, decimal>("HEI-454", 3, 3.00m)
                };
            var checkout = new Checkout(offers);

            checkout.Scan(Cherries);
            checkout.Scan(Cherries);
            checkout.Scan(Cherries);
            Assert.That(checkout.Total, Is.EqualTo(0.5m));
        }

        [Test]
        public void Have_An_Offer_Of_3_Heinz_For_3GBP_When_I_Add_Three_Heinz_The_Total_Is_3GBP()
        {
            var offers = new List<Tuple<string, int, decimal>>
                {
                    new Tuple<string, int, decimal>("C", 3, 0.50m),
                    new Tuple<string, int, decimal>("HEI-454", 3, 3.00m)
                };
            var checkout = new Checkout(offers);

            checkout.Scan(HeinzBeans);
            checkout.Scan(HeinzBeans);
            checkout.Scan(HeinzBeans);
            Assert.That(checkout.Total, Is.EqualTo(3m));
        }
    }


}
