// https://atcoder.jp/contests/abc060/tasks/abc060_b

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1], x.[2])
|> fun (a, b, c) ->
    [1..a]
    |> Seq.tryFind (fun y -> (b * y + c) % a = 0)
    |> fun x ->
        match x with
        | Some(_) -> "YES"
        | None -> "NO"
|> printfn "%s"

// https://atcoder.jp/contests/abc060/submissions/9936473
