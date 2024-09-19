#!/bin/bash

# Environment variables
UNITY_EXE_PATH="${UNITY_EXE_PATH:-/Applications/Unity/Hub/Editor/2022.3.30f1/Unity.app/Contents/MacOS/Unity}"
PROJECT_PATH=com.chartboost.core.canary     # The unity project where this package is being used

# Generate .csproj files
"$UNITY_EXE_PATH" -quit -batchmode -nographics -projectPath $PROJECT_PATH -executeMethod Packages.Rider.Editor.RiderScriptEditor.SyncSolution

# Generate index.md
chmod +x com.chartboost.unity.ci/scripts~/generate-docfx-index.sh
com.chartboost.unity.ci/scripts~/generate-docfx-index.sh "com.chartboost.core/Documentation" "Chartboost.Core.html"

# Generate Documentation
echo "Generating documentation.."
dotnet docfx com.chartboost.core/Documentation/docfx.json

echo "Generating documentation complete"
