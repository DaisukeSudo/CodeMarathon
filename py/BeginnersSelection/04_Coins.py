# https://atcoder.jp/contests/abs/tasks/abc087_b

from math import floor

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))
  filter = lambda self, f: W(filter(f, self.v))

W((input(), input(), input(), input())) \
  .map(int) \
  .let(tuple) \
  .let(lambda t:
    W(range(0, min([t[0], floor(t[3] / 500)]) + 1)) \
      .map(lambda i:
        W(range(0, min([t[1], floor((t[3] - 500 * i) / 100)]) + 1)) \
          .filter(lambda j: t[3] - 500 * i - 100 * j <= 50 * t[2]) \
          .let(list)
          .let(len) \
          .v
      ) \
      .let(sum) \
      .v
  ) \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21624786
