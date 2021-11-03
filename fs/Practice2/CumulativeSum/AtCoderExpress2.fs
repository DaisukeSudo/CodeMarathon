// https://atcoder.jp/contests/abc106/tasks/abc106_d

stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1], x.[2])
|> fun (n, m, q) ->
  (
    ([1..m] |> List.map (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1]))),
    ([1..q] |> List.map (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1]))),
    Array.init (n + 2) (fun _ -> Array.create (n + 1) 0)
  )
  |> fun (lrs, pqs, acc) ->
    [
      lrs |> List.iter (fun (l, r) -> acc.[l].[r] <- acc.[l].[r] + 1);
      seq { for i in n..(-1)..1 do for j in 1..n -> (i, j) } |> Seq.iter (fun (i, j) ->
        acc.[i].[j] <- acc.[i].[j] + acc.[i + 1].[j] + acc.[i].[j - 1] - acc.[i + 1].[j - 1]
      )
    ]
    |> fun _ ->
      pqs |> List.iter (fun (p, q) -> acc.[p].[q] |> printfn "%d")

// https://atcoder.jp/contests/abc106/submissions/27011086
