# https://atcoder.jp/contests/abs/tasks/arc089_a

(
  readline()
  |> a -> parse.(Int, a)
  |> n -> map(_ -> readline() |> split |> a -> parse.(Int, a) |> a -> tuple(a...), 1 : n)
  |> ts -> foldl((p, n) -> (
    p != nothing && (
        ((t1, x1, y1), (t2, x2, y2),) ->
          (t2 & 1) == ((x2 + y2) & 1) &&
            ((t2 - t1) >= (abs(x2 - x1) + abs(y2 - y1)))
      )(p, n)
      ? n
      : nothing
  ), ts, init = (0, 0, 0))
  |> x -> (x != nothing ? "Yes" : "No")
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22656790
