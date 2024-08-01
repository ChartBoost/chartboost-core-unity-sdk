package com.chartboost.core.unity.initialization

import com.chartboost.core.initialization.ModuleConfiguration

interface ModuleEventConsumer {
    fun initialize(configuration:ModuleConfiguration, completion: ModuleInitializeCompleter)

    fun updateCredentials(credentials: String)
}
