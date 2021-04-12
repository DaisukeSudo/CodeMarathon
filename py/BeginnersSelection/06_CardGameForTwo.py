# https://atcoder.jp/contests/abs/tasks/abc088_b

from functools import reduce

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))
  fold = lambda self, f, init: W(reduce(f, self.v, init))

W([input(), input()][1].split()) \
  .map(int) \
  .let(lambda x: sorted(x, reverse=True)) \
  .fold(lambda t, x: (
    lambda a, b, i:
      (a + x, b, i + 1)
      if i % 2 == 0 else
      (a, b + x, i + 1)
  )(t[0], t[1], t[2]), (0, 0, 0)) \
  .let(lambda t: t[0] - t[1]) \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21717312
