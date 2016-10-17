#!/bin/bash

for testdir in `find . -name 'project.lock.json' | xargs dirname | grep '\.Tests'`; do
    dotnet test "$testdir" || return 1
done
