// https://atcoder.jp/contests/abc046/tasks/abc046_b

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1])
|> fun (n, k) -> [1..(n - 1)] |> Seq.fold (fun a _ -> a * (k - 1)) k
|> printfn "%d"

// https://atcoder.jp/contests/abc046/submissions/9641416
