Salsa20 bias finder
-------------------

Finds biases in 4 round Salsa20.

Written by CodesInChaos (Christian Winnerlein). Licensed CC0 / Public domain.

Inspired by the paper [Salsa20 Cryptanalysis: New Moves and Revisiting Old Styles - Subhamoy Maitra, Goutam Paul, Willi Meier](https://eprint.iacr.org/2015/217).

[Results](https://github.com/CodesInChaos/SalsaBias/tree/master/Results)
------------------------------------------------------------------------

* 4 rounds of Salsa20 with 2^25 samples
* 4 rounds of Salsa20 with 2^28 samples

Comparison with the paper
-------------------------

In section 4 the paper lists several biases for 4 round Salsa20:

    Bits             Paper      This program
    7,31 -> 1,14     -0.1314    -0.1302
    8,31 -> 11,17    +1.1642    +0.1659
    7,31 -> 6,26     -0.1954    -0.1949
    8,31 -> 11,26    -0.1914    -0.1950

This seem consistent, even if the deviations are slightly bigger than I expected.