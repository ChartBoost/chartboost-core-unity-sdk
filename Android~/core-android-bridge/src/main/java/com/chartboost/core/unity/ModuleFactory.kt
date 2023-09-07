package com.chartboost.core.unity

import android.content.Context
import com.chartboost.core.error.ChartboostCoreError
import com.chartboost.core.error.ChartboostCoreException
import com.chartboost.core.initialization.InitializableModule
import org.json.JSONObject
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine

@Suppress("unused")
class ModuleFactory {

    companion object {
        @JvmStatic
        fun makeUnityModule(id: String, version: String, initializer: ModuleInitializerConsumer) {
            val unityModule = object : InitializableModule {
                override val moduleId = id
                override val moduleVersion = version
                override suspend fun initialize(context: Context): Result<Unit> {
                    val exception = awaitInitialization(initializer)
                    return if (exception == null)
                        Result.success(Unit)
                    else
                        Result.failure(exception)
                }

                override fun updateProperties(configuration: JSONObject) {
                    TODO("Not yet implemented")
                }

                private suspend fun awaitInitialization(initializer: ModuleInitializerConsumer) : Exception? {
                    return suspendCoroutine { continuation ->
                        initializer.initialize(object : ModuleInitializeCompletion {
                            override fun completed(error: CoreErrorUnity?) {
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

            BridgeCBC.addModule(unityModule)
        }
    }
}




