// https://atcoder.jp/contests/typical90/tasks/typical90_n

let _ = stdin.ReadLine()
let a = stdin.ReadLine().Split() |> Array.map int64 |> Array.sort
let b = stdin.ReadLine().Split() |> Array.map int64 |> Array.sort

Array.zip a b
|> Array.map (fun (ai, bi) -> abs (ai - bi))
|> Array.sum
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/32882116
