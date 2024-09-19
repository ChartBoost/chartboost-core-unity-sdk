@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.initialization

import com.chartboost.core.unity.error.CoreErrorUnity

interface ModuleInitializeCompleter {
    fun completed(error: CoreErrorUnity?)
}
