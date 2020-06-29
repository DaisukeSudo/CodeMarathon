// https://atcoder.jp/contests/abc055/tasks/abc055_b

stdin.ReadLine()
|> int64
|> fun x -> [1L..x]
|> Seq.reduce (fun a b -> a * b % 1000000007L)
|> printfn "%d"

// https://atcoder.jp/contests/abc055/submissions/9505328
