package com.chartboost.core.unity

import com.chartboost.core.initialization.ModuleInitializationConfiguration

interface ModuleInitializerConsumer {
    fun initialize(configuration:ModuleInitializationConfiguration, completion: ModuleInitializeCompletion)
}
