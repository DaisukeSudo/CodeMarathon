// https://atcoder.jp/contests/abc149/tasks/abc149_b

stdin.ReadLine().Split() |> Array.map int64 |> fun x -> (x.[0], x.[1], x.[2])
|> fun (a, b, k) ->
  [max 0L (a - k); max 0L (b - (max 0L (k - a)))]
|> Seq.map string
|> String.concat " "
|> printfn "%s"

// https://atcoder.jp/contests/abc149/submissions/29731802
