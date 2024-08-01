#!/bin/bash

# Environment variables
UNITY_EXE_PATH="${UNITY_EXE_PATH:-/Applications/Unity/Hub/Editor/2022.3.30f1/Unity.app/Contents/MacOS/Unity}"
PROJECT_PATH=com.chartboost.core.canary     # The unity project where this package is being used

# Generate .csproj files
"$UNITY_EXE_PATH" -quit -batchmode -nographics -projectPath $PROJECT_PATH -executeMethod Packages.Rider.Editor.RiderScriptEditor.SyncSolution

# Copy files
echo "Copying files..."
cp com.chartboost.core/README.md com.chartboost.core/Documentation/index.md
cp com.chartboost.core/CONTRIBUTING.md com.chartboost.core/Documentation/CONTRIBUTING.md
cp com.chartboost.core/LICENSE.md com.chartboost.core/Documentation/LICENSE.md

# Update links in index.md
echo "Updating links..."
sed -i '' 's|\(Documentation/\)\([^)]*\)|\2|g' com.chartboost.core/Documentation/index.md

# Generate Documentation
echo "Generating documentation.."
dotnet docfx com.chartboost.core/Documentation/docfx.json

echo "Generating documentation complete"
