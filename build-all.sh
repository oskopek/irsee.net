#!/bin/bash

for dir in `find . -name 'project.lock.json' | xargs dirname`; do
    dotnet build "$dir" || return 1
done
