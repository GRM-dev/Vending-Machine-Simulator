using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core
{
    /// <summary>
    /// Kontrola przepywu monet w automacie.
    /// Weryfikuje, przyjmuje monety i wydaje reszte.
    /// 
    /// </summary>
    class CoinController
    {
        /// <summary>
        /// When the v param is correct coin value than returns true
        /// </summary>
        /// <param name="value">coin value to check</param>
        /// <returns>true if coin is correct</returns>
        public static Boolean isCoinCorrect(double value)
        {
            if (value > 0 && (value == 0.10 || value == 0.20 ||
                value == 0.50 || value == 1.00 || value == 2.00 || value == 5.00))
            {
                return true;
            } return false;
        }

        /// <summary>
        /// Checks if specified value is correct coin
        /// </summary>
        /// <param name="value">string</param>
        /// <returns>true if coin is correct</returns>
        public static Boolean isCoinCorrect(string value)
        {
            ValueHandler prop = new ValueHandler(value);
            if (prop.PropertyType != ValueHandler.ValueTypes.STRING && isCoinCorrect(Convert.ToDouble(prop.Value)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Throws temporary container to the main money container.
        /// </summary>
        public static void addTempToMainCoinDepository()
        {
            MainCoinDepository += TempCoinDepository;
            TempCoinDepository = 0f;
        }

        /// <summary>
        /// Gets temporary depository
        /// </summary>
        /// <returns>total value of temp depo</returns>
        public static double ReturnTempDepo()
        {
            double returnV = TempCoinDepository;
            TempCoinDepository = 0;
            return returnV;
        }

        /// <summary>
        /// When user throws coin into machine this method adds it to temp depo
        /// </summary>
        /// <param name="m"></param>
        public static void ThrowInCoin(double m)
        {
            TempCoinDepository += m;
        }

        /// <summary>
        /// Main secure Coin Depo
        /// </summary>
        public static double MainCoinDepository { get; private set; }
        /// <summary>
        /// Temporary coin depo which current client uses
        /// </summary>
        public static double TempCoinDepository { get; private set; }
    }
}
