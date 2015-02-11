using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using VendingMachine.Core;
using VendingMachine.Core.Products;
using VendingMachine.XAML;

namespace VendingMachine.Simulations
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientGenerator
    {
        private SimulationFlyout simulationFlyout;
        private Random rand = new Random();
        private Thread thread;
        private int servicedClients = 0;
        private MainPage mPage = VMachine.instance.MWindow.VMMainPage;
        private SimulationFlyout simF = VMachine.instance.MWindow.SimulationFlyoutP;
        private Dispatcher disp = Application.Current.Dispatcher;
        private ArrayList clients = new ArrayList();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="simulationFlyout"></param>
        public ClientGenerator(SimulationFlyout simulationFlyout)
        {
            this.simulationFlyout = simulationFlyout;
        }

        /// <summary>
        /// Start new generator thread
        /// </summary>
        public void Start()
        {
            if (thread == null)
            {
                thread = new Thread(StartGenerator);
            }
            else if (thread.IsAlive)
            {
                thread.Interrupt();
            }
            thread = new Thread(StartGenerator);
            thread.Start();
        }

        /// <summary>
        /// start client generating
        /// </summary>
        public void StartGenerator()
        {
            while (simulationFlyout.SimulationRunning)
            {
                Client client = new Client();
                clients.Add(client);
                int productID = (int)client.Product;
                if (ProductsController.hasProduct(productID))
                {
                    throwCoins(client);
                    double price = ProductsController.getProductData(productID).Product_Price;
                    if (CoinController.hasEnoughMoney(price))
                    {
                        Product product = ProductsController.getProduct(productID);
                        CoinController.transferMoneyToMainDepo(product.PData.Product_Price);
                        disp.BeginInvoke((Action)(() => ProductsController.RemoveProduct(productID, 1)));
                    }
                    CoinController.ReturnTempDepo();
                }
                servicedClients++;
                disp.BeginInvoke((Action)(() => simF.ServicedClientsCount.Text = servicedClients.ToString()));
                int selSpeed = 1;
                disp.BeginInvoke((Action)(() => selSpeed = Convert.ToInt32(simF.slValue.Value)));
                Console.Out.WriteLine("" + selSpeed);
                double rnD = rand.NextDouble() * 10;
                int sleepTime = Convert.ToInt32(1000 * rnD / selSpeed);
                try
                {
                    Thread.Sleep(sleepTime);
                }
                catch (ThreadInterruptedException e) { }
            }
        }

        private void throwCoins(Client client)
        {
            foreach (double coin in client.Wallet)
            {
                CoinController.ThrowInCoin(coin);
                string consoleTitle = "Stan konta: ";
                string currentTempDepo = CoinController.TempCoinDepository.ToString();
                disp.BeginInvoke((Action)(() => mPage.Console_1.Text = consoleTitle + currentTempDepo));
            }
        }
    }
}
