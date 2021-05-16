# https://atcoder.jp/contests/abs/tasks/abc088_b

(
  (readline(), readline())[2]
  |> split
  |> a -> parse.(Int, a)
  |> a -> sort(a, rev = true)
  |> a -> foldl(((a, b, i), x) -> (
    i % 2 == 0
      ? (a + x, b, i + 1)
      : (a, b + x, i + 1)
  ), a, init = (0, 0, 0))
  |> x -> x[1] - x[2]
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22656992
