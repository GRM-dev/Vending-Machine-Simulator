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
using VendingMachine.Core.Configuration;
using VendingMachine.Core.Products;
using VendingMachine.VMDialogs;
using VendingMachine.XAML;

namespace VendingMachine.Simulations
{
    /// <summary>
    /// Clients simulation
    /// </summary>
    public class ClientGenerator
    {
        private SimulationFlyout simulationFlyout;
        private Random rand = new Random();
        private Thread thread;
        private MainPage mPage = VMachine.instance.MWindow.VMMainPage;
        private SimulationFlyout simF = VMachine.instance.MWindow.SimulationFlyoutP;
        private Dispatcher disp = Application.Current.Dispatcher;
        private ArrayList clients = new ArrayList();
        private int selSpeed=1;
        private int soldProducts;
        private double earnedMoney;


        /// <summary>
        /// Client Generation start
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
            string cL="0";
             string pL="0";
            disp.Invoke(DispatcherPriority.Normal, new Action(delegate() { cL =simF.ClientsMaxLimit.Text; }));
            disp.Invoke(DispatcherPriority.Normal, new Action(delegate() { pL = simF.ProductsMaxLimit.Text; }));
            int clientsLimit = Convert.ToInt32(cL);
            int soldProductsLimit=Convert.ToInt32(pL);
            clientsLimit = clientsLimit == 0 ? int.MaxValue : clientsLimit;
            soldProductsLimit = soldProductsLimit == 0 ? int.MaxValue : soldProductsLimit;
            while (simulationFlyout.SimulationState==SimulationFlyout.RUNNING&&clients.Count<clientsLimit&&soldProducts<soldProductsLimit)
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
                        soldProducts++;
                        earnedMoney+=price;
                    }
                    CoinController.ReturnTempDepo();
                }
                updateParams();
                if (rand.Next(0,300) == 200)
                {
                   disp.BeginInvoke((Action)(() => simF.SimulationState = SimulationFlyout.STOPPED));
                   disp.BeginInvoke((Action)(() => VMDialogManager.ShowExceptionMessage(new Exception("Błąd!"))));
                   ConfigProperties.Update(ConfigPropertyType.WORKS,"True");
                }
                double rnD = rand.NextDouble() * 10;
                int sleepTime = Convert.ToInt32(1000 * rnD / selSpeed);
                try
                {
                    Thread.Sleep(sleepTime);
                }
                catch (ThreadInterruptedException e) { }
            }
            simF.SimulationState = SimulationFlyout.STOPPED;
        }

        private void updateParams()
        {
            disp.BeginInvoke((Action)(() => simF.ServicedClientsCount.Text = clients.Count.ToString()));
            disp.BeginInvoke((Action)(() => selSpeed = Convert.ToInt32(simF.SpeedValue.Text)));
            disp.BeginInvoke((Action)(() => simF.SoldProductsCount.Text = soldProducts.ToString()));
            disp.BeginInvoke((Action)(() => simF.EarnedMoney.Text = earnedMoney.ToString()));
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
