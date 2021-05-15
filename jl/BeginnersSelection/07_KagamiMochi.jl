# https://atcoder.jp/contests/abs/tasks/abc085_b

(
  readline()
  |> x -> parse.(Int, x)
  |> n -> map(_ -> readline(), 1 : n)
  |> unique
  |> length
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22589243
