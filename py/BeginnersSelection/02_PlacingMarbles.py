# https://atcoder.jp/contests/abs/tasks/abc081_a

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))

W(input()) \
  .map(int) \
  .let(sum) \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21602387
