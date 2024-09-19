@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.factory

interface ModuleFactoryEventConsumer {
    fun makeModule(className: String, completion: ModuleFactoryMakeModuleCompleter)
}
