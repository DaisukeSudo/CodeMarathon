// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_1_C

stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (v, e) ->
  (
    Array.init v (fun _ -> Array.create v 4000000000L),
    seq { 1..e }
    |> Seq.map(fun _ ->
      stdin.ReadLine().Split()
      |> fun x -> (int x.[0], int x.[1], int64 x.[2])
    )
  )
|> fun (graph, stds) ->
  (
    seq { 0..graph.Length - 1 } |> Seq.iter (fun i -> graph.[i].[i] <- 0L),
    stds |> Seq.iter (fun (s, t, d) -> graph.[s].[t] <- d)
  )
  |> fun _ ->
    seq {
      for k in 0..graph.Length - 1 do
      for i in 0..graph.Length - 1 do
      for j in 0..graph.Length - 1 do
      yield (i, j, k)
    }
    |> Seq.iter (fun (i, j, k) ->
      graph.[i].[j] <- min graph.[i].[j] (graph.[i].[k] + graph.[k].[j])
    )
  |> fun _ ->
    if seq { 0..graph.Length - 1 } |> Seq.exists (fun i -> graph.[i].[i] < 0L)
    then "NEGATIVE CYCLE"
    else (
      graph
      |> Array.map (fun row ->
        row
        |> Array.map (fun x -> if x > 2000000000L then "INF" else x.ToString())
        |> String.concat " "
      )
      |> String.concat "\n"
    )
  |> printfn "%s"
