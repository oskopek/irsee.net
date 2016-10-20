#!/bin/bash

projectLocks="`find . -name 'project.lock.json'`"

if [ -z "$projectLocks" ]; then
    echo "Could not find project locks. Maybe execute 'dotnet restore' first?"
    exit 1
fi

for testdir in `echo "$projectLocks" | xargs realpath | xargs dirname | grep '\.Tests'`; do
    dotnet test "$testdir" || return 1
done
