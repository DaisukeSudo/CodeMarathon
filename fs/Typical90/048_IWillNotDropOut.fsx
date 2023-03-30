// https://atcoder.jp/contests/typical90/tasks/typical90_av

let n, k = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let ab = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1])

ab
|> Array.collect (fun (a, b) -> [| b; a - b |])
|> Array.sortDescending
|> Array.take k
|> Array.sum
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/40157723
