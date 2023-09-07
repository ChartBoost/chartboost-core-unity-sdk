namespace Chartboost.Core.Initialization
{
    /// <summary>
    /// Configuration with parameters needed by the Chartboost Core SDK on initialization.
    /// </summary>
    public class SDKConfiguration
    {
        /// <summary>
        /// The Chartboost App ID which should match the value on the Chartboost dashboard.
        /// </summary>
        public string ChartboostApplicationIdentifier { get; }

        public SDKConfiguration(string chartboostApplicationIdentifier)
        {
            ChartboostApplicationIdentifier = chartboostApplicationIdentifier;
        }
    }
}
