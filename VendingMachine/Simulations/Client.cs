using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Products;

namespace VendingMachine.Simulations
{
    /// <summary>
    /// Client constructor (randomizing wallet and request)
    /// </summary>
    public class Client
    {
        Random rand = new Random();
        double[] coinValues={0.2,0.5,1,2,5};

        /// <summary>
        /// Client class
        /// </summary>
        public Client()
        {
            Wallet = new ArrayList();
            int moneysCount=rand.Next(10);
            for (double i = 1; i < moneysCount; i++)
            {
                Wallet.Add(coinValues[rand.Next(0,4)]);
            }
              
            Array values = Enum.GetValues(typeof(ProductE));
            ProductE randPoduct = (ProductE)values.GetValue(rand.Next(values.Length));
            Product = randPoduct;
        }

        /// <summary>
        /// Wallet array with random coins
        /// </summary>
        public ArrayList Wallet { get; set; }
        /// <summary>
        /// Requested random product from product list
        /// </summary>
        public ProductE Product { get; set; }
    }
}
