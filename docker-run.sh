#!/bin/bash
set -e
docker build -t registry.gitlab.com/oskopek/irsee.net .
docker run -it registry.gitlab.com/oskopek/irsee.net

