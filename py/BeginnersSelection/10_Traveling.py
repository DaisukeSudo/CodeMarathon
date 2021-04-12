# https://atcoder.jp/contests/abs/tasks/arc089_a

from functools import reduce

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))
  fold = lambda self, f, init: W(reduce(f, self.v, init))

W(input()) \
  .let(int) \
  .let(lambda n: range(0, n)) \
  .map(lambda _: W(input().split()).map(int).let(tuple).v) \
  .fold(lambda p, n:
    n if p != None and (
      lambda t1, x1, y1, t2, x2, y2: 
        (t2 & 1) == ((x2 + y2) & 1) and
          ((t2 - t1) >= (abs(x2 - x1) + abs(y2 - y1)))
      )(p[0], p[1], p[2], n[0], n[1], n[2])
      else None
    , (0, 0, 0)
  ) \
  .let(lambda x: "Yes" if x != None else "No") \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21719931
