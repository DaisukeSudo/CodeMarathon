# https://atcoder.jp/contests/abs/tasks/abc085_c

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))
  find = lambda self, f: W(next(filter(f, self.v), None))

W(input().split()) \
  .map(int) \
  .let(tuple) \
  .let(lambda t: (t[0], t[1] / 1000)) \
  .let(lambda t: (
    lambda n, y:
      W(range(0, n + 1)) \
        .find(lambda a: 
          W(y - n - 9 * a) \
            .let(lambda x: x % 4 == 0 and x >= 0 and n - a - x / 4 >= 0) \
            .v
        ) \
        .let(lambda a:
          W((y - n - 9 * a) / 4) \
            .let(int) \
            .let(lambda b: "{0:d} {1:d} {2:d}".format(a, b, n - a - b)) \
            .v
          if a != None else
          "-1 -1 -1"
        ) \
        .v
  )(t[0], t[1])) \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21718207
