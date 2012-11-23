using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckoutKata.Domain
{
    public class Checkout
    {
        private readonly List<Tuple<string, int, decimal>> _offers;

        private static Dictionary<string, decimal> _prices = new Dictionary<string, decimal>
            {
                {"A", 0.5m},
                {"B", 0.3m},
                {"C", 0.2m},
                {"D", 0.15m},
                {"HEI-454", 1.49m}
            }; 

        private readonly Dictionary<string, int> _items = new Dictionary<string, int>();
        private bool offersMode;

        public Checkout()
        {
            
        }

        public Checkout(List<Tuple<string, int, decimal>> offers )
        {
            _offers = offers;
            offersMode = true;
        }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                if (!offersMode)
                {
                    int noOfA = 0;
                    _items.TryGetValue("A", out noOfA);
                    var offersA = noOfA/3;
                    var remA = (noOfA%3);
                    total += offersA*1.25m + remA*0.50m;

                    int noOfB = 0;
                    _items.TryGetValue("B", out noOfB);
                    var offersB = (int) (noOfB/2);
                    var remB = (noOfB%2);
                    total += offersB*0.45m + remB*0.30m;
                    total += _items.Where(s => s.Key != "A" && s.Key != "B").Select(s => _prices[s.Key]).Sum();
                }
                else
                {
                    int noOfC = 0;
                    Tuple<string, int, decimal> offer;
                    
                    _items.TryGetValue("C", out noOfC);
                    var offersB = (int)(noOfC / 2);
                    var remB = (noOfC % 2);
                    total += offersB * 0.45m + remB * 0.30m;
                    total += _items.Where(s => s.Key != "C").Select(s => _prices[s.Key]).Sum();
                }

                
                return total;
            }
        }

        public void Scan(string sku)
        {
            AddItem(sku);
        }

        private void AddItem(string sku)
        {
            if (!_items.ContainsKey(sku))
            {
                _items.Add(sku, 0);
            }
            _items[sku] += 1;
        }
    }
}
