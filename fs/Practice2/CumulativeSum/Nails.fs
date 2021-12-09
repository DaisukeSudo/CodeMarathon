// https://atcoder.jp/contests/joi2012ho/tasks/joi2012ho4

stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (n, m) ->
  (Array.init (n + 3) (fun _ -> Array.create (n + 3) 0))
  |> fun acc ->
    [1..m]
    |> List.map (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1], x.[2]))
    |> List.iter (fun (a, b, x) ->
      [
        acc.[a].[b]                 <- acc.[a].[b]                 + 1;
        acc.[a].[b + 1]             <- acc.[a].[b + 1]             - 1;
        acc.[a + x + 1].[b]         <- acc.[a + x + 1].[b]         - 1;
        acc.[a + x + 1].[b + x + 2] <- acc.[a + x + 1].[b + x + 2] + 1;
        acc.[a + x + 2].[b + 1]     <- acc.[a + x + 2].[b + 1]     + 1;
        acc.[a + x + 2].[b + x + 2] <- acc.[a + x + 2].[b + x + 2] - 1;
      ]
      |> ignore
    )
    |> fun _ -> [1..n + 2] |> List.iter (fun a -> [1..n + 2] |> List.iter (fun b ->
      acc.[a].[b] <- acc.[a].[b] + acc.[a].[b - 1]
    ))
    |> fun _ -> [1..n + 2] |> List.iter (fun a -> [1..n + 2] |> List.iter (fun b ->
      acc.[a].[b] <- acc.[a].[b] + acc.[a - 1].[b]
    ))
    |> fun _ -> [1..n + 2] |> List.iter (fun a -> [1..n + 2] |> List.iter (fun b ->
      acc.[a].[b] <- acc.[a].[b] + acc.[a - 1].[b - 1]
    ))
    |> fun _ -> acc |> Array.sumBy (Array.filter ((<) 0) >> Array.length)
|> printfn "%d"

// https://atcoder.jp/contests/joi2012ho/submissions/27776205
