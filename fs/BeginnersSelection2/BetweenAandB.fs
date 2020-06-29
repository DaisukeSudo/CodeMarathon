// https://atcoder.jp/contests/abc048/tasks/abc048_b

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int64
|> fun x -> (x.[0], x.[1], x.[2])
|> fun (a, b, x) -> (b / x) - if a = 0L then -1L else ((a - 1L) / x)
|> printfn "%d"

// https://atcoder.jp/contests/abc048/submissions/9879532
