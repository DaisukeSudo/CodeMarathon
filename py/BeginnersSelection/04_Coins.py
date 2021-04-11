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
  .let(lambda t: (
    lambda a, b, c, x:
      W(range(0, min(a, floor(x / 500)) + 1)) \
        .map(lambda i:
          W(range(0, min(b, floor((x - 500 * i) / 100)) + 1)) \
            .filter(lambda j: x - 500 * i - 100 * j <= 50 * c) \
            .let(list) \
            .let(len) \
            .v
        ) \
        .let(sum) \
        .v
  )(t[0], t[1], t[2], t[3])) \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21625388
