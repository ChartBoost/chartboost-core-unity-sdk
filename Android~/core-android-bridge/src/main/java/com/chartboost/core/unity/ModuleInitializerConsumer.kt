package com.chartboost.core.unity

interface ModuleInitializerConsumer {
    fun initialize(completion: ModuleInitializeCompletion)
}
