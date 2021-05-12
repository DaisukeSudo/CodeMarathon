# https://atcoder.jp/contests/abs/tasks/abc081_a

(
  readline()
  |> s -> [c for c = s]
  |> a -> sum(x -> parse.(Int, x), a)
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22542702
