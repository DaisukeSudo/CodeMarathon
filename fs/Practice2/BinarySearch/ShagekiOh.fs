// https://atcoder.jp/contests/abc023/tasks/abc023_d

stdin.ReadLine()
|> int64
|> fun n ->
  [1L..n]
  |> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int64 |> fun x -> (x.[0], x.[1]))
  |> fun balloons ->
    (
      balloons |> List.map (fun (h, s) -> h) |> List.max,
      balloons |> List.map (fun (h, s) -> h + s * (n - 1L)) |> List.max
    )
    |> fun (lv, rv) ->
      (rv - lv |> double, 2.) |> System.Math.Log |> ceil |> int
      |> fun limI ->
        [1..limI]
        |> List.fold (
          fun (lv, rv) _ ->
            lv + (rv - lv) / 2L
            |> fun mv ->
              balloons
              |> List.sortBy (fun (h, s) -> (mv - h) / s)
              |> List.mapi (fun i (h, s) -> (mv - h) / s - (int64 i) >= 0L)
              |> List.forall (id)
            |> fun matched ->
              if matched
              then (lv, mv)
              else (mv, rv)
        ) (lv, rv)
        |> fun (_, v) -> v
|> printfn "%d"

// https://atcoder.jp/contests/abc023/submissions/15388752
