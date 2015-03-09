My program measures the probability that changing a bit in the input flips a bit in the output.
But the paper defines the bias as `p(11 | x1) = (1 + bias) / 2`.

We get three equations:

Total probability is 1:

    p00 + p01 + p10 + p11 = 1

Bits in the input and output are unbiased when looked at in isolation.
This follows from the tested function being a permutation.

    p00 + p01 = p10 + p11
    p00 + p10 = p01 + p11

Combining these we get:

    p01 = p10
    p00 = p11

And can now compute the conditional probability:

    p(11 | x1) = p11 / (p01 + p11)
               = p11 / 0.5 = p00 + p11
               = 1 - (p10 + p01)

And the bias:

    bias = 1 - 2 * p(11 | x1)
         = 2 * (p10 + p01) - 1

Switching from probabilities to values we can measure:

    Bias = 2 * (count / total) - 1
         = (count / expectancyValue) - 1
         = (count - expectancyValue) / expectancyValue

For the error I'm using `sqrt(count) / expectancyValue`. This matches the standard deviation for individual items.
Note that `max(abs(bias))` will be several times bigger than that even for unbiased data.