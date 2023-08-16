using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chartboost.Core
{
    public class SDKConfiguration
    {
        public string ChartboostApplicationIdentifier { get; }

        public SDKConfiguration(string chartboostApplicationIdentifier)
        {
            ChartboostApplicationIdentifier = chartboostApplicationIdentifier;
        }
    }
}
