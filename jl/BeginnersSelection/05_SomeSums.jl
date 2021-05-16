# https://atcoder.jp/contests/abs/tasks/abc083_b

(
  readline()
  |> split
  |> a -> parse.(Int, a)
  |> ((n, a, b),) ->
    filter(i -> (
      [parse.(Int, c) for c = string(i)]
      |> sum
      |> x -> a <= x && x <= b
    ), (1 : n))
  |> sum
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22656922
