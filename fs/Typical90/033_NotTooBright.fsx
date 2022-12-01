// https://atcoder.jp/contests/typical90/tasks/typical90_ag

let h, w = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]

let f x = (x >>> 1) + (x &&& 1)

if h = 1 || w = 1 then h * w else f h * f w
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/36904806
// https://atcoder.jp/contests/typical90/submissions/36907768
