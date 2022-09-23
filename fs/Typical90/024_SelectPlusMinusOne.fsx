// https://atcoder.jp/contests/typical90/tasks/typical90_x

let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let a = stdin.ReadLine().Split() |> Array.map int
let b = stdin.ReadLine().Split() |> Array.map int

let c = [for i in 0 .. n - 1 -> abs (a.[i] - b.[i])] |> List.sum
let d = k - c

if d >= 0 && d % 2 = 0 then "Yes" else "No"
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/35060669
