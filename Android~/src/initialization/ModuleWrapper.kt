@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.initialization

import android.content.Context
import com.chartboost.core.error.ChartboostCoreException
import com.chartboost.core.initialization.Module
import com.chartboost.core.initialization.ModuleConfiguration
import com.chartboost.core.unity.error.CoreErrorUnity
import com.chartboost.mediation.unity.logging.LogLevel
import com.chartboost.mediation.unity.logging.UnityLoggingBridge
import org.json.JSONObject
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine

@Suppress("unused")
class ModuleWrapper {

    companion object {

        private val TAG = ModuleWrapper::class.simpleName

        @JvmStatic
        fun wrapUnityModule(id: String, version: String, moduleEventConsumer: ModuleEventConsumer) : Module {
            val unityModule = object : Module {
                override val moduleId = id
                override val moduleVersion = version

                override suspend fun initialize(context: Context, moduleConfiguration: ModuleConfiguration): Result<Unit> {
                    UnityLoggingBridge.log(TAG, "Wrapped module: $moduleId attempting initialization", LogLevel.VERBOSE)
                    val exception = awaitInitialization(moduleConfiguration, moduleEventConsumer)
                    return if (exception == null)
                        Result.success(Unit)
                    else
                        Result.failure(exception)
                }

                override fun updateCredentials(context: Context, credentials: JSONObject) {
                    moduleEventConsumer.updateCredentials(credentials.let { credentials.toString() })
                    UnityLoggingBridge.log(TAG, "Wrapped module: $moduleId updated credentials", LogLevel.VERBOSE)
                }

                private suspend fun awaitInitialization(moduleConfiguration: ModuleConfiguration, moduleEventConsumer: ModuleEventConsumer) : Exception? {
                    return suspendCoroutine { continuation ->
                        moduleEventConsumer.initialize(moduleConfiguration, object :
                            ModuleInitializeCompleter {
                            override fun completed(error: CoreErrorUnity?) {
                                UnityLoggingBridge.log(TAG, "Wrapped module: $moduleId initialization result obtained", LogLevel.VERBOSE)
                                error?.let {
                                    continuation.resume(ChartboostCoreException(it))
                                } ?: run {
                                    continuation.resume(null)
                                }
                            }
                        })
                    }
                }
            }

            UnityLoggingBridge.log(TAG, "Wrapped module ${unityModule.moduleId}", LogLevel.VERBOSE)
            return unityModule
        }
    }
}




