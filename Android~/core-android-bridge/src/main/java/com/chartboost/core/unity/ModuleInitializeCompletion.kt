package com.chartboost.core.unity

import com.chartboost.core.error.ChartboostCoreError

interface ModuleInitializeCompletion {
    fun completed(error: ChartboostCoreError?)
}
