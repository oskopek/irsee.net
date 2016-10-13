#!/bin/bash

find . -name 'project.lock.json' -type 'f' | xargs rm -rf
find . -name 'obj' -type 'd' | xargs rm -rf
find . -name 'bin' -type 'd' | xargs rm -rf

