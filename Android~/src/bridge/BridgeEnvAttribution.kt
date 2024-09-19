@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.bridge

import com.chartboost.core.ChartboostCore
import com.chartboost.core.unity.result.ResultString
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Suppress("unused")
class BridgeEnvAttribution {
    companion object {
        @JvmStatic
        fun getAdvertisingIdentifier(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val advertisingIdentifier = ChartboostCore.attributionEnvironment.getAdvertisingIdentifier()
                stringResult.onResult(advertisingIdentifier)
            }
        }

        @JvmStatic
        fun getUserAgent(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val userAgent = ChartboostCore.attributionEnvironment.getUserAgent()
                stringResult.onResult(userAgent)
            }
        }
    }
}
