# https://atcoder.jp/contests/abs/tasks/abc086_a

(
  readline()
  |> split
  |> a -> parse.(Int, a)
  |> a -> map(x -> x & 1, a)
  |> a -> reduce(&, a)
  |> x -> (x == 1 ? "Odd" : "Even")
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22542398
