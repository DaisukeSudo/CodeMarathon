# https://atcoder.jp/contests/abc149/tasks/abc149_b

(
  readline() |> split |> a -> parse.(Int64, a) |> a -> (a[1], a[2], a[3])
  |> ((a, b, k),) -> (
    "$(max(0, a - k)) $(max(0, b - max(0, (k - a))))";
  )
  |> println
)

# https://atcoder.jp/contests/abc149/submissions/29733995
