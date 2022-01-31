// https://atcoder.jp/contests/s8pc-5/tasks/s8pc_5_b

(
  fun (x1, y1) (x2, y2) -> ((x2 - x1) ** 2. + (y2 - y1) ** 2.) |> sqrt
)
|> fun distance ->
  stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])
  |> fun (n, m) ->
    (
      ([|0..n - 1|] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map float |> fun x -> (x.[0], x.[1], x.[2]))),
      ([|0..m - 1|] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map float |> fun x -> (x.[0], x.[1], System.Double.MaxValue)))
    )
  |> fun (ns, ms) ->
    [
      [0..m - 1] |> List.iter (fun i1 -> [0..n - 1] |> List.iter (fun i2 ->
        (ms.[i1], ns.[i2]) |> fun ((x1, y1, r1), (x2, y2, r2)) ->
          (distance (x1, y1) (x2, y2)) |> fun d ->
            ms.[i1] <- (x1, y1, r1 |> min (d - r2))
      ));
      [0..m - 1] |> List.iter (fun i1 -> [0..m - 1] |> List.filter ((<) i1) |> List.iter (fun i2 ->
        (ms.[i1], ms.[i2]) |> fun ((x1, y1, r1), (x2, y2, r2)) ->
          (distance (x1, y1) (x2, y2)) |> ((*) 0.5) |> fun d2 ->
            [
              ms.[i1] <- (x1, y1, r1 |> min (d2));
              ms.[i2] <- (x2, y2, r2 |> min (d2));
            ]
            |> ignore
      ));
    ]
    |> fun _ ->
      [
        ns |> Array.map (fun (_, _, x) -> x);
        ms |> Array.map (fun (_, _, x) -> x);
      ]
      |> Array.concat
      |> Array.min
|> printfn "%f"

// https://atcoder.jp/contests/s8pc-5/submissions/28975939
