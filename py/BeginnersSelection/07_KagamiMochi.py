# https://atcoder.jp/contests/abs/tasks/abc085_b

from functools import reduce

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))

W(input()) \
  .let(int) \
  .let(lambda n: range(0, n)) \
  .map(lambda _: input()) \
  .let(set) \
  .let(len) \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21717507
