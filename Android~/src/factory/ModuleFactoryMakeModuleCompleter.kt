@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.factory

import com.chartboost.core.initialization.Module

interface ModuleFactoryMakeModuleCompleter {
    fun completed(module: Module?)
}
