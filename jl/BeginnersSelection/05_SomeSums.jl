# https://atcoder.jp/contests/abs/tasks/abc083_b

(
  readline()
  |> split
  |> a -> parse.(Int, a)
  |> t -> (
    (n, a, b) ->
      filter(i -> (
        [parse.(Int, c) for c = string(i)]
        |> sum
        |> x -> a <= x && x <= b
      ), (1 : n))
  )(t[1], t[2], t[3])
  |> sum
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22577839
