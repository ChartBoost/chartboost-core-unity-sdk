package com.chartboost.core.unity

import com.chartboost.core.ChartboostCore
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Suppress("unused")
class BridgeEnvAnalytics {
    companion object{
        @JvmStatic
        fun getAdvertisingIdentifier(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val advertisingIdentifier = ChartboostCore.analyticsEnvironment.getAdvertisingIdentifier()
                stringResult.onResult(advertisingIdentifier)
            }
        }

        @JvmStatic
        fun getUserAgent(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val userAgent = ChartboostCore.analyticsEnvironment.getUserAgent()
                stringResult.onResult(userAgent)
            }
        }

        @JvmStatic
        fun getLimitAdTrackingEnabled(booleanResult: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                val limitAdTrackingEnabled = ChartboostCore.analyticsEnvironment.getLimitAdTrackingEnabled()
                booleanResult.onResult(limitAdTrackingEnabled)
            }
        }

        @JvmStatic
        fun getVendorIdentifier(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val vendorIdentifier = ChartboostCore.analyticsEnvironment.getVendorIdentifier()
                stringResult.onResult(vendorIdentifier)
            }
        }

        @JvmStatic
        fun getVendorIdentifierScope(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val vendorIdentifierScope = ChartboostCore.analyticsEnvironment.getVendorIdentifierScope()
                stringResult.onResult(vendorIdentifierScope.toString())
            }
        }
    }
}
