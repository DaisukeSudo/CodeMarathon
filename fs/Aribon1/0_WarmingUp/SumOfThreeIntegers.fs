// https://atcoder.jp/contests/abc051/tasks/abc051_b

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1])
|> fun (k, s) ->
  [0..k]
  |> Seq.map (fun x ->
    [0..k]
    |> Seq.filter (fun y -> (s >= x + y) && (k >= s - x - y))
    |> Seq.length
  )
  |> Seq.reduce (+)
|> printfn "%A"

// 2 2
// 0 0 2 true
// 0 1 1 true
// 0 2 0 true
// 1 0 1 true
// 1 1 0 true
// 1 2 -1 false
// 2 0 0 true
// 2 1 -1 false
// 2 2 -2 false
// 6

// https://atcoder.jp/contests/abc051/submissions/11465802
