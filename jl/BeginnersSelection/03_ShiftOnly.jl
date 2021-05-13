# https://atcoder.jp/contests/abs/tasks/abc081_b

(
  [readline(), readline()][2]
  |> split
  |> a -> parse.(Int, a)
  |> a -> map(x -> string(x, base = 2), a)
  |> a -> map(x -> length(x) - findlast('1', x), a)
  |> minimum
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22558015
