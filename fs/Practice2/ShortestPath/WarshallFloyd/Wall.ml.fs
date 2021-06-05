// https://atcoder.jp/contests/abc079/tasks/abc079_d

let readInput = fun () ->
  (
    (stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])),
    (Array.init 10 (fun _ -> stdin.ReadLine().Split() |> Array.map int))
  )
  |> fun ((h, w), costs) ->
    (
      costs,
      seq { 1..h } |> Seq.map(fun _ -> stdin.ReadLine().Split() |> Array.map int)
    )

let warshallFloyd = fun (graph : int [][]) ->
  seq {
    for k in 0..graph.Length - 1 do
    for i in 0..graph.Length - 1 do
    for j in 0..graph.Length - 1 do
    yield (i, j, k)
  }
  |> Seq.iter (fun (i, j, k) ->
    graph.[i].[j] <- min graph.[i].[j] (graph.[i].[k] + graph.[k].[j])
  )
  |> fun _ -> graph

let repaint = fun (costs : int [][], wall) ->
  wall |> Seq.sumBy (Seq.sumBy (fun x ->
    if x = -1 then 0 else costs.[x].[1]
  ))

()
|> readInput
|> fun (costs, wall) -> (costs |> warshallFloyd, wall)
|> repaint
|> printfn "%d"

// https://atcoder.jp/contests/abc079/submissions/23187063
