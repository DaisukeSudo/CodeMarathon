// https://atcoder.jp/contests/gigacode-2019/tasks/gigacode_2019_d

stdin.ReadLine().Split() |> fun x -> (int x.[0], int x.[1], int64 x.[2], int64 x.[3])
|> fun (h, w, k, v) ->
  [
    [| Array.create (w + 1) 0L |];
    [| 1..h |] |> Array.map (fun _ ->  [[| 0L |]; stdin.ReadLine().Split() |> Array.map int64] |> Array.concat)
  ]
  |> Array.concat
  |> fun (acc : int64[][]) ->
    (
      [1..h] |> List.iter (fun i -> [1..w] |> List.iter (fun j ->
        acc.[i].[j] <- acc.[i].[j] + acc.[i - 1].[j] + acc.[i].[j - 1] - acc.[i - 1].[j - 1]
      )),
      (fun (x1, y1, x2, y2) -> acc.[x1].[y1] + acc.[x2].[y2] - acc.[x1].[y2] - acc.[x2].[y1]),
      (fun (x1, y1, x2, y2) -> (x2 - x1) * (y2 - y1)),
      (fun x -> System.Math.Log(double x, 2.) |> ceil |> int),
      [| 0 |]
    )
    |> fun (_, cost, area, limit, accArea) ->
      [1..limit h + 1] |> List.fold (fun (lx1, rx1, _) _ ->
        lx1 + (rx1 - lx1) / 2 |> fun x1 ->
          [1..limit w + 1] |> List.fold (fun (ly1, ry1, _) _ ->
            ly1 + (ry1 - ly1) / 2 |> fun y1 ->
              [1..limit (h - x1) + 1] |> List.fold (fun (lx2, rx2, _) _ ->
                lx2 + (rx2 - lx2) / 2 |> fun x2 ->
                  [1..limit (w - y1) + 1] |> List.fold (fun (ly2, ry2, a) _ ->
                    ly2 + (ry2 - ly2) / 2 |> fun y2 ->
                      area (x1, y1, x2, y2) |> fun curArea ->
                        if int64 curArea * k + cost (x1, y1, x2, y2) <= v
                        then (accArea.[0] <- max accArea.[0] curArea) |> fun () -> (y2, ry2, true)
                        else (ly2, y2 - 1, a)
                  ) (y1 + 1, w + 1, false)
                  |> fun (_, _, a) -> if a then (x2, rx2, a) else (lx2, x2 - 1, a)
              ) (x1 + 1, h + 1, false)
              |> fun (_, _, a) -> if a then (ly1, y1, a) else (y1 + 1, ry1, a)
          ) (0, w, false)
          |> fun (_, _, a) -> if a then (lx1, x1, a) else (x1 + 1, rx1, a)
      ) (0, h, false)
      |> fun _ -> accArea.[0]
|> printfn "%d"

// https://atcoder.jp/contests/gigacode-2019/submissions/27167608
// https://atcoder.jp/contests/gigacode-2019/submissions/27170972
