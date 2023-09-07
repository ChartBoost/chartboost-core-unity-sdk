package com.chartboost.core.unity

import com.chartboost.core.error.ChartboostCoreErrorContract

class CoreErrorUnity(
    override val code: Int,
    override val message: String,
    override val cause: String?,
    override val resolution: String) : ChartboostCoreErrorContract {
}
