# https://atcoder.jp/contests/abs/tasks/arc065_a

from functools import reduce
from itertools import chain

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))
  flat_map = lambda self, f: self.map(f).let(chain.from_iterable)
  filter = lambda self, f: W(filter(f, self.v))
  fold = lambda self, f, init: W(reduce(f, self.v, init))

W(input().strip()) \
  .let(lambda s:
    W(["eraser", "erase", "dreamer", "dream"]) \
      .fold(lambda acc, word:
        W(acc).flat_map(lambda x:
          W(x.split(word)).filter(lambda x: x != "").v
        ).v
        , [s]
      ) \
      .v
  ) \
  .let(list) \
  .let(len) \
  .let(lambda x: "YES" if x == 0 else "NO") \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21719418
