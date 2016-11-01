#!/bin/bash
set -e
. ./clean.sh
dotnet restore
. ./build-all.sh
. ./test-all.sh

