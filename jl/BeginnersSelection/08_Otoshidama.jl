# https://atcoder.jp/contests/abs/tasks/abc085_c

using Printf

(
  readline()
  |> split
  |> x -> parse.(Int, x)
  |> ((n, y),) -> (n, y / 1000)
  |> ((n, y),) -> (
    (0 : n)
    |> as -> findfirst(a -> (
      (y - n - 9 * a)
      |> x -> x % 4 == 0 && x >= 0 && n - a - x / 4 >= 0
    ), as)
    |> a -> (
      a != nothing
        ? (
          (a - 1)
          |> a ->
            ((y - n - 9 * a) / 4)
            |> Int
            |> b -> (a, b, n - a - b)
        )
        : (-1, -1, -1)
    )
  )
  |> (((a, b, c),) -> @sprintf("%d %d %d", a, b, c))
  |> println
)

# https://atcoder.jp/contests/abs/submissions/22649190
