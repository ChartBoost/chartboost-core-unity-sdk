@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.bridge

import com.chartboost.core.ChartboostCore
import com.chartboost.core.ChartboostCoreLogLevel
import com.chartboost.core.initialization.Module
import com.chartboost.core.initialization.ModuleObserver
import com.chartboost.core.initialization.SdkConfiguration
import com.chartboost.mediation.unity.logging.LogLevel
import com.chartboost.mediation.unity.logging.UnityLoggingBridge
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.Main
import kotlinx.coroutines.launch

@Suppress("unused")
class BridgeCBC {
    companion object
    {
        private val TAG = BridgeCBC::class.simpleName
        private val modulesToInitialize = mutableListOf<Module>()

        @JvmStatic
        fun addModule(module: Module){
            modulesToInitialize.add(module)
            UnityLoggingBridge.log(TAG, "Added Module ${module.moduleId} to Initialization List", LogLevel.VERBOSE)
        }

        @JvmStatic
        fun clearModules(){
            modulesToInitialize.clear()
            UnityLoggingBridge.log(TAG, "Cleared module initialization list, new size: ${modulesToInitialize.size}", LogLevel.VERBOSE)
        }

        @JvmStatic
        fun getNativeSDKConfiguration(chartboostApplicationIdentifier: String, skippedModuleIdentifiers: Array<String>): SdkConfiguration {
            UnityLoggingBridge.log(TAG, "Created SDKConfiguration for Chartboost Core", LogLevel.VERBOSE)
            return SdkConfiguration(chartboostApplicationIdentifier, modulesToInitialize, skippedModuleIdentifiers.toSet())
        }

        @JvmStatic
        fun getLogLevel() : Int {
            ChartboostCore.logLevel.let {
                UnityLoggingBridge.log(TAG, "LogLevel is $it", LogLevel.VERBOSE)
                return it.value
            }
        }

        @JvmStatic
        fun setLogLevel(value: Int){
            val logLevel = ChartboostCoreLogLevel.fromInt(value)
            ChartboostCore.logLevel = logLevel
            UnityLoggingBridge.log(TAG, "LogLevel set to $logLevel", LogLevel.VERBOSE)
        }

        @JvmStatic
        fun initializeSdk(sdkConfiguration: SdkConfiguration, observer: ModuleObserver) {
            CoroutineScope(Main).launch {
                UnityPlayer.currentActivity.let {
                    UnityLoggingBridge.log(TAG, "Attempting to Initialize Chartboost Core with ${modulesToInitialize.size} modules", LogLevel.VERBOSE)
                    ChartboostCore.initializeSdk(it, sdkConfiguration, observer)
                }
            }
        }
    }
}
