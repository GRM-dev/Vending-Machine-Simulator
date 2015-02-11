using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Configuration
{
    /// <summary>
    /// Enum of available properties to config program properties
    /// </summary>
    public enum ConfigPropertyType
    {
        WINDOW_FULLSCREEN = 0, WINDOW_HEIGHT = 2, WINDOWS_WIDTH = 3, SLOTS_COUNT = 4, MONEY_COLLECTED = 5, WORKS = 6, SERVICE_PASSWD = 7,
        SLOT_SIZE=8, CALL_FOR_REFILL=9
    }
}
