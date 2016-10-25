#!/usr/bin/python3

from mnist import MNIST
mndata = MNIST('./mnist')
train_features, train_labels = mndata.load_training()
test_features, test_labels = mndata.load_testing()

train = [train_features[i]+[train_labels[i]] for i in range(0, len(train_features))]
test = [test_features[i]+[test_labels[i]] for i in range(0, len(test_features))]

import csv

quoting=csv.QUOTE_NONE
outdir='mnist/'

with open(outdir + 'mnist.train.csv', 'w', newline="") as myfile:
    wr = csv.writer(myfile, quoting=quoting)
    [wr.writerow(t) for t in train]

with open(outdir + 'mnist.test.csv', 'w', newline="") as myfile:
    wr = csv.writer(myfile, quoting=quoting)
    [wr.writerow(t) for t in test]

with open(outdir + 'mnist.all.csv', 'w', newline="") as myfile:
    wr = csv.writer(myfile, quoting=quoting)
    [wr.writerow(t) for t in train]
    [wr.writerow(t) for t in test]

# import numpy as np
# import scipy.misc as smp

# Create a 28x28x1 array of 8 bit unsigned integers
# data = np.zeros((28,28,3), dtype=np.uint8 )

# sample = train_features[2]

# cnt = 0
# for i in range(0, 28):
#    for j in range(0, 28):
#        data[i, j] = [sample[cnt], 0, 0]
#        cnt += 1
#
# img = smp.toimage(data)
# img.show()

