// https://atcoder.jp/contests/abc144/tasks/abc144_d

stdin.ReadLine().Split() |> Array.map float |> fun x -> (x.[0], x.[1], x.[2])
|> fun (a, b, x) ->
  [
    (2. * b / a) - (2. * x / (a * a * a));
    (a * b * b / (2. * x));
  ]
|> List.map (atan >> ((*) (180. / System.Math.PI)))
|> List.max
|> printfn "%f"

// https://atcoder.jp/contests/abc144/submissions/29166279
