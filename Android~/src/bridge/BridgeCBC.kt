package com.chartboost.core.unity.bridge

import com.chartboost.core.ChartboostCore
import com.chartboost.core.ChartboostCoreLogLevel
import com.chartboost.core.initialization.Module
import com.chartboost.core.initialization.ModuleObserver
import com.chartboost.core.initialization.SdkConfiguration
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.Main
import kotlinx.coroutines.launch


@Suppress("unused")
class BridgeCBC {
    companion object
    {
        private val modules = mutableListOf<Module>()

        @JvmStatic
        fun addModule(module: Module){
            modules.add(module)
        }

        @JvmStatic
        fun clearModules(){
            modules.clear()
        }

        @JvmStatic
        fun getNativeSDKConfiguration(chartboostApplicationIdentifier: String, skippedModuleIdentifiers: Array<String>): SdkConfiguration {
            return SdkConfiguration(chartboostApplicationIdentifier, modules, skippedModuleIdentifiers.toSet())
        }

        @JvmStatic
        fun getLogLevel() : Int {
            ChartboostCore.logLevel.let {
                return it.value
            }
        }

        @JvmStatic
        fun setLogLevel(logLevel: Int){
            ChartboostCore.logLevel = ChartboostCoreLogLevel.fromInt(logLevel)
        }

        @JvmStatic
        fun initializeSdk(sdkConfiguration: SdkConfiguration, observer: ModuleObserver) {
            CoroutineScope(Main).launch {
                UnityPlayer.currentActivity.let {
                    ChartboostCore.initializeSdk(it, sdkConfiguration, observer)
                }
            }
        }
    }
}
