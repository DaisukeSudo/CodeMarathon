// https://atcoder.jp/contests/abc175/tasks/abc175_b

(
  (stdin.ReadLine() |> int),
  (stdin.ReadLine().Split() |> Array.map int)
)
|> fun (n, ls) ->
  seq {
    for i in     0..n - 3 do
    for j in i + 1..n - 2 do
    for k in j + 1..n - 1 do
    if (ls.[i], ls.[j], ls.[k]) |> fun (li, lj, lk) ->
      li <> lj &&
      li <> lk &&
      lj <> lk &&
      li < lj + lk &&
      lj < li + lk &&
      lk < li + lj
    then yield (i, j, k)
  }
|> Seq.length
|> printfn "%d"

// https://atcoder.jp/contests/abc175/submissions/30456110
