# https://atcoder.jp/contests/abs/tasks/abc086_a

from functools import reduce

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))
  reduce = lambda self, f: W(reduce(f, self.v))

W(input().split()) \
  .map(lambda x: int(x) & 1) \
  .reduce(lambda a, x: a & x) \
  .let(lambda x: "Odd" if x == 1 else "Even") \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21596116
