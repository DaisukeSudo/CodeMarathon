// https://atcoder.jp/contests/abc070/tasks/abc070_b

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> x.[0], x.[1], x.[2], x.[3]
|> fun (a, b, c, d) -> (max a c), (min b d)
|> fun (s, e) -> e - s
|> max 0
|> printfn "%d"

// https://atcoder.jp/contests/abc070/submissions/9504629
