# https://atcoder.jp/contests/abs/tasks/abc083_b

class W:
  def __init__(self, v): self.v = v
  let = lambda self, f: W(f(self.v))
  map = lambda self, f: W(map(f, self.v))
  filter = lambda self, f: W(filter(f, self.v))

W(input().split()) \
  .map(int) \
  .let(tuple) \
  .let(lambda t: (
    lambda n, a, b:
      W(range(1, n + 1)) \
        .filter(lambda i:
          W(i) \
            .let(str) \
            .map(int) \
            .let(sum) \
            .let(lambda x: a <= x and x <= b) \
            .v
        ) \
        .let(sum) \
        .v
  )(t[0], t[1], t[2])) \
  .let(print)

# https://atcoder.jp/contests/abs/submissions/21625598
