# https://atcoder.jp/contests/abs/tasks/abc081_b

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))

W([input(), input()][1].split()) \
  .map(int) \
  .map(bin) \
  .map(lambda x: len(x) - x.rindex("1") - 1) \
  .let(min) \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21596351
