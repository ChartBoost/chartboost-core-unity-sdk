@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.factory

import com.chartboost.core.ChartboostCore
import com.chartboost.core.initialization.Module
import com.chartboost.core.initialization.ModuleFactory
import com.chartboost.mediation.unity.logging.LogLevel
import com.chartboost.mediation.unity.logging.UnityLoggingBridge
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine

@Suppress("unused")
class ModuleFactoryUnity : ModuleFactory {


    fun setModuleFactoryUnity()
    {
        try {
            val property = ChartboostCore::class.java.getDeclaredField(NONNATIVEMODULEFACTORY)
            property.isAccessible = true
            property.set(ChartboostCore, this)
            UnityLoggingBridge.log(TAG, "ModuleFactory successfully set", LogLevel.VERBOSE)
        }
        catch (exception: Exception)
        {
            UnityLoggingBridge.logException(TAG, "Failed to set nonNativeModuleFactory with Exception: $exception")
        }
    }

    override suspend fun makeModule(className: String): Module? {
        moduleFactoryEventConsumer?.let {
            val module = awaitModuleCreation(className, it)
            UnityLoggingBridge.log(TAG, "ModuleFactory created module: $className", LogLevel.VERBOSE)
            return module
        } ?: run {
            UnityLoggingBridge.log(TAG, "ModuleFactory not set, failed to create module: $className", LogLevel.ERROR)
            return null
        }
    }

    private suspend fun awaitModuleCreation(className: String, moduleFactoryEventConsumer: ModuleFactoryEventConsumer) : Module?{
        return suspendCoroutine { continuation ->
            moduleFactoryEventConsumer.makeModule(className, object : ModuleFactoryMakeModuleCompleter {
                override fun completed(module: Module?) {
                    module?.let {
                        continuation.resume(module)
                    } ?: run {
                        continuation.resume(null)
                    }
                }
            })
        }
    }

    companion object {
        private val TAG = Companion::class.java.simpleName
        private const val NONNATIVEMODULEFACTORY = "nonNativeModuleFactory"
        private var moduleFactoryEventConsumer: ModuleFactoryEventConsumer? = null

        @JvmStatic
        fun setModuleFactoryEventConsumer(moduleFactoryObserver: ModuleFactoryEventConsumer){
            this.moduleFactoryEventConsumer = moduleFactoryObserver
            UnityLoggingBridge.log(TAG, "Set ModuleFactory Consumer", LogLevel.VERBOSE)
        }
    }
}
