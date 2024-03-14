#!/bin/bash

# Check if PUBLIC_REPOSITORY_PATH is not set or is empty
if [ -z "${PUBLIC_REPOSITORY_PATH}" ]; then
    echo "The \$PUBLIC_REPOSITORY_PATH variable is unset or empty. Assigning default value 'Public'."
    PUBLIC_REPOSITORY_PATH=Public
fi

# Ensure the directory exists
mkdir -p "${PUBLIC_REPOSITORY_PATH}"

# com.chartboost.core 
rsync -av --progress --verbose --delete \
--exclude='.git' \
com.chartboost.core/ "${PUBLIC_REPOSITORY_PATH}/"

