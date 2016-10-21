#!/bin/bash

projectLocks="`find . -name 'project.lock.json'`"

if [ -z "$projectLocks" ]; then
    echo "Cannot find project lock files. Probably need to run 'dotnet restore' first."
    exit 1
fi

for dir in `echo "$projectLocks" | xargs realpath | xargs dirname`; do
    dotnet publish "$dir" 2>&1
    dotnet pack "$dir" || return 1
done
#return 0

