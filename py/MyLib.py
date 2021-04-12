# ----- 基本 -----

# object.let = lambda self, f : f(self) 
# > TypeError: can't set attributes of built-in/extension type 'object'

from functools import reduce
from itertools import chain

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))
  flat_map = lambda self, f: self.map(f).let(chain.from_iterable)
  filter = lambda self, f: W(filter(f, self.v))
  find = lambda self, f: W(next(filter(f, self.v), None))
  reduce = lambda self, f: W(reduce(f, self.v))
  fold = lambda self, f, init: W(reduce(f, self.v, init))

W(input()) \
  .let(lambda x: x) \
  .let(print)
