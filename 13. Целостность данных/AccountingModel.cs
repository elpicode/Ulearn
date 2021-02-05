using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get => price;
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        public int NightsCount
        {
            get => nightsCount; 
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        public double Discount
        {
            get => discount;
            set
            {
                if (value > 100)
                    throw new ArgumentException();
                discount = value;
                total = price * nightsCount * (1 - discount / 100);
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        public double Total
        {
            get => price * nightsCount * (1 - discount / 100); 
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                if ( price * nightsCount == 0 )
                    throw new ArgumentException();
                total = value;
                discount = 100 - 100 * total / (price * nightsCount);
                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }
    }
}