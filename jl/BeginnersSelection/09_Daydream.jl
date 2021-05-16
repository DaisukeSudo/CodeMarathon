# https://atcoder.jp/contests/abs/tasks/arc065_a

(
  readline()
  |> strip
  |> s ->
    ["eraser", "erase", "dreamer", "dream"]
    |> ws -> foldl((acc, w) -> (
      map(x -> (
        split(x, w) |> a -> filter(x -> x != "", a)
      ), acc)
      |> a -> vcat(a...)
    ), ws, init = [s])
  |> length
  |> x -> (x == 0 ? "YES" : "NO")
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22656272
