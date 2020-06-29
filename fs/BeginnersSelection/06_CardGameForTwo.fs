// https://atcoder.jp/contests/abs/tasks/abc088_b

stdin.ReadLine()
|> ignore
|> fun () -> stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> Array.sortWith (fun a b -> b - a)
|> Array.mapi (fun i x -> (i, x))
|> Array.fold (fun (a, b) (i, x) ->
  (
    if i % 2 = 0
    then (a + x, b)
    else (a, b + x)
  )
) (0, 0)
|> fun (a, b) -> a - b
|> printfn "%d"

// ----------

stdin.ReadLine() |> ignore |> fun () -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.sortWith (fun a b -> b - a) |> Array.mapi (fun i x -> (i, x)) |> Array.fold (fun (a, b) (i, x) -> if i % 2 = 0 then (a + x, b) else (a, b + x) ) (0, 0) |> fun (a, b) -> a - b |> printfn "%d"

// https://atcoder.jp/contests/abs/submissions/9414786

// ----------

// mapi はあるのに foldi はない。。
