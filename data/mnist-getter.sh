#!/bin/bash

outdir='mnist'
origdir="`pwd`"
url='http://yann.lecun.com/exdb/mnist/'
data_arr='train-images-idx3-ubyte.gz train-labels-idx1-ubyte.gz t10k-images-idx3-ubyte.gz t10k-labels-idx1-ubyte.gz'

rm -rf "$outdir" > /dev/null
mkdir -p "$outdir" > /dev/null

cd "$outdir"

for i in $data_arr; do
    echo "Downloading: $i"
    curl "$url$i" > "$i"
    echo "Extracting: $i"
    gunzip "$i"
done

cd "$origdir"

python3 mnist-reader.py
