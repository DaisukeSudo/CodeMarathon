// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_1_C

stdin.ReadLine().Split()
|> Array.map int
|> fun x -> (x.[0], x.[1])
|> fun (n, m) ->
  (
    Array.init n (fun _ -> Array.create n (System.Int32.MaxValue / 2)),
    seq { 1..m } |> Seq.map(fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1], x.[2]))
  )
|> fun (graph, abts) ->
  (
    seq { 0..graph.Length - 1 } |> Seq.iter (fun i -> graph.[i].[i] <- 0),
    abts |> Seq.iter (fun (a, b, t) -> ((graph.[a - 1].[b - 1] <- t), (graph.[b - 1].[a - 1] <- t)) |> ignore)
  )
  |> fun _ ->
    seq {
      for k in 0..graph.Length - 1 do
      for i in 0..graph.Length - 1 do
      for j in 0..graph.Length - 1 do
      yield (i, j, k)
    }
    |> Seq.iter (fun (i, j, k) -> graph.[i].[j] <- min graph.[i].[j] (graph.[i].[k] + graph.[k].[j]))
  |> fun _ -> graph |> (Seq.map Seq.max) |> Seq.min
|> printfn "%d"

// https://atcoder.jp/contests/abc012/submissions/22985956
