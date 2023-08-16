package com.chartboost.core.unity

import com.chartboost.core.ChartboostCore
import com.chartboost.core.initialization.InitializableModule
import com.chartboost.core.initialization.InitializableModuleObserver
import com.chartboost.core.initialization.SdkConfiguration
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.Main
import kotlinx.coroutines.launch

class BridgeCBC {
    @Suppress("unused")
    companion object
    {
        private val modules = mutableListOf<InitializableModule>()
        @JvmStatic
        fun addModule(module: InitializableModule){
            modules.add(module)
        }

        @JvmStatic
        fun initializePreferences(){
            UnityPlayer.currentActivity.let {
                ChartboostCore.initializePreferences(it)
            }
        }

        @JvmStatic
        fun initializeSdk(sdkConfiguration: SdkConfiguration, observer: InitializableModuleObserver) {
            CoroutineScope(Main).launch {
                UnityPlayer.currentActivity.let {
                    ChartboostCore.initializeSdk(it, sdkConfiguration, modules, observer)
                    modules.clear()
                }
            }
        }
    }
}
