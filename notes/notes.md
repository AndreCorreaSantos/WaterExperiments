### IF we divide the integer portion and use it as input for the noise generator, we can create blocky landscapes

c7c88a063c4cfd9972acff29ed579f4e399ddfe2

![1729442531412](image/notes/1729442531412.png)

### If we interpolate along the four points on the cell, we get a smoother noise

8587e74c5846515395d81d43eb8c47fa0b798e3d

![1729446638693](image/notes/1729446638693.png)

### Decreasing the frequency of the noise and increasing amplitude -> dividing the grid space and increasing height multiplier

c7dc0cfbea7ff76872b8c58b35b15132278b61e1

![1729446836008](image/notes/1729446836008.png)

## Value Noise vs Gradient Noise

- Use value noise as the gradient for the actual noise in the scene.
- Value noise - used for height or normal maps, the noise value is more hectic.
- Gradient Noise - used for terrain generation

### First attempt at implementing gradient noise from scratch, got very interesting blocky result --> because of absence of a fade function.

b2aaa28ae34d56e70bcc1cecac9302e4e02ed955

- very bad performance as well, computer is suffering.

![1729455175109](image/notes/1729455175109.png)

### By fixing an error with the cell indexing, we now get proper gradient noise - cell divisions are no longer visible

991087ab1808db82ee62f052020ca00708f3c58f

![1729457535973](image/notes/1729457535973.png)

## Changing scroll logic:

- currently all of the noises can only scroll horizontally

![1729458210145](image/notes/1729458210145.gif)


### Volumetric Noise to allow vertical scrolling
