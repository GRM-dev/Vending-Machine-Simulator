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
        /// <param name="v">coin value to check</param>
        /// <returns>true if coin is correct</returns>
        public static Boolean isCoinCorrect(float v)
        {
            if (v > 0 && (v == 0.10 || v == 0.20 ||
                v == 0.50 || v == 1.00 || v == 2.00 || v == 5.00))
            {
                return true;
            } return false;
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
        /// Returns the rest of money al if operation cancelled whole stored temp depo value
        /// </summary>
        /// <param name="r"></param>
        public static void ReturnRest(float r)
        {
            MainCoinDepository -= r;
        }

        /// <summary>
        /// Gets temporary depository
        /// </summary>
        /// <returns>total value of temp depo</returns>
        public static float ReturnTempDepo()
        {
            return TempCoinDepository;
        }

        /// <summary>
        /// When user throws coin into machine this method adds it to temp depo
        /// </summary>
        /// <param name="m"></param>
        public static void ThrowInCoin(float m)
        {
            TempCoinDepository += m;
        }

        public static float MainCoinDepository { get; set; }
        public static float TempCoinDepository { get; set; }
    }
}
