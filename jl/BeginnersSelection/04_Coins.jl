# https://atcoder.jp/contests/abs/tasks/abc087_b

(
  (readline(), readline(), readline(), readline())
  |> a -> parse.(Int, a)
  |> ((a, b, c, x),) -> (
    (0 : min(a, floor(x / 500)))
    |> is -> map(i -> (
      (0 : min(b, floor((x - 500 * i) / 100)))
      |> js -> filter(j -> (
        x - 500 * i - 100 * j <= 50 * c
      ), js)
      |> length
    ), is)
    |> sum
  )
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22656867
