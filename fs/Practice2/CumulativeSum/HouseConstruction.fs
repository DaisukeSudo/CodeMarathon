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
      [0..limit h] |> List.fold (fun (lx1, rx1, ax1) _ ->
        lx1 + (rx1 - lx1) / 2 |> fun x1 ->
          [0..limit w] |> List.fold (fun (ly1, ry1, ay1) _ ->
            ly1 + (ry1 - ly1) / 2 |> fun y1 ->
              [0..limit (h - x1)] |> List.fold (fun (lx2, rx2, ax2) _ ->
                lx2 + (rx2 - lx2) / 2 |> fun x2 ->
                  [0..limit (w - y1)] |> List.fold (fun (ly2, ry2, ay2) _ ->
                    ly2 + (ry2 - ly2) / 2 |> fun y2 ->
                      area (x1, y1, x2, y2) |> fun curArea ->
                        if int64 curArea * k + cost (x1, y1, x2, y2) <= v
                        then (accArea.[0] <- max accArea.[0] curArea) |> fun () -> (y2, ry2, true)
                        else (ly2, y2, ay2)
                  ) (y1, w + 1, false)
                  |> fun (_, _, a) -> if a then (x2, rx2, true) else (lx2, x2, ax2)
              ) (x1, h + 1, false)
              |> fun (_, _, a) -> if a then (ly1, y1, true) else (y1, ry1, ay1)
          ) (0, w, false)
          |> fun (_, _, a) -> if a then (lx1, x1, true) else (x1, rx1, ax1)
      ) (0, h, false)
      |> fun _ -> accArea.[0]
|> printfn "%d"

// https://atcoder.jp/contests/gigacode-2019/submissions/27167608
// https://atcoder.jp/contests/gigacode-2019/submissions/27174387
