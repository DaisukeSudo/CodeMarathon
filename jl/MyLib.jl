# ----- 基本 -----

(
  readline()
  |> split
  |> a -> parse.(Int, a)
  |> a -> sort(a, rev = true)
  |> a -> map(x -> x * 2, a)
  |> println
)
