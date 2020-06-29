// https://atcoder.jp/contests/joi2008yo/tasks/joi2008yo_d

(
  stdin.ReadLine()
  |> int
  |> fun m -> [| 1..m |]
  |> Array.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
  //|> Array.sortWith (fun (x1, y1) (x2, y2) -> if x1 <> x2 then x1 - x2 else y1 - y2)
  ,
  stdin.ReadLine()
  |> int
  |> fun n -> [| 1..n |]
  |> Array.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
  //|> Array.sortWith (fun (x1, y1) (x2, y2) -> if x1 <> x2 then x1 - x2 else y1 - y2)
)
|> fun (constellations, stars) ->
  (
    constellations.[0]
    |> (
      fun (x0, y0) ->
        constellations
        |> Array.map (fun (x, y) -> (x - x0, y - y0))
    ),
    stars |> Set.ofSeq
  )
  |> fun (relativePositions, starSet) ->
    [0..(stars.Length - 1)]
    |> List.find (
      fun i ->
        [1..(relativePositions.Length - 1)]
        |> List.forall (
          fun j ->
            (
              stars.[i],
              relativePositions.[j]
            )
            |> fun ((x, y), (dx, dy)) ->
              starSet.Contains (x + dx, y + dy)
        )
    )
  |> fun i -> (constellations.[0], stars.[i])
  |> fun ((x0, y0), (x1, y1)) -> (x1 - x0, y1 - y0)
  |> fun (dx, dy) -> printfn "%d %d" dx dy

// https://atcoder.jp/contests/joi2008yo/submissions/12173427
