namespace Chartboost.Core.Environment
{
    /// <summary>
    /// Network connection type.
    /// </summary>
    public enum NetworkConnectionType
    {
        /// <summary>
        /// Unknown connection type.
        /// </summary>
        Unknown = 0,
        
        /// <summary>
        /// Wired network connection.
        /// </summary>
        Wired = 1,
        
        /// <summary>
        /// WiFi network connection.
        /// </summary>
        Wifi = 2,
        
        /// <summary>
        /// Cellular network connection of unknown generation.
        /// </summary>
        CellularUnknown = 3,
        
        /// <summary>
        /// 2G cellular network connection.
        /// </summary>
        Cellular2G = 4,
        
        /// <summary>
        /// 3G cellular network connection.
        /// </summary>
        Cellular3G = 5,
        
        /// <summary>
        /// 4G cellular network connection.
        /// </summary>
        Cellular4G = 6,
        
        /// <summary>
        /// 5G cellular network connection.
        /// </summary>
        Cellular5G = 7,
    }
}
