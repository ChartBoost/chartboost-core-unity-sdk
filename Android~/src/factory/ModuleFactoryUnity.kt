package com.chartboost.core.unity.factory

import android.util.Log
import com.chartboost.core.ChartboostCore
import com.chartboost.core.initialization.Module
import com.chartboost.core.initialization.ModuleFactory
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine

@Suppress("unused")
class ModuleFactoryUnity : ModuleFactory {

    private val TAG:String = Companion::class.java.name

    fun setModuleFactoryUnity()
    {
        try {
            val property = ChartboostCore::class.java.getDeclaredField("nonNativeModuleFactory")
            property.isAccessible = true
            property.set(ChartboostCore, this)
        }
        catch (exception: Exception)
        {
            Log.e(TAG, "Failed to set nonNativeModuleFactory with Exception: $exception")
        }
    }

    override suspend fun makeModule(className: String): Module? {
        moduleFactoryEventConsumer?.let {
            val module = awaitModuleCreation(className, it)
            return module
        } ?: run {
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
        private var moduleFactoryEventConsumer: ModuleFactoryEventConsumer? = null

        @JvmStatic
        fun setModuleFactoryEventConsumer(moduleFactoryObserver: ModuleFactoryEventConsumer){
            this.moduleFactoryEventConsumer = moduleFactoryObserver
        }
    }
}
