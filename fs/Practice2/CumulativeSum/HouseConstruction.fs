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
      seq { for i in 1..h do for j in 1..w -> (i, j) } |> Seq.iter (fun (i, j) ->
        acc.[i].[j] <- acc.[i].[j] + acc.[i - 1].[j] + acc.[i].[j - 1] - acc.[i - 1].[j - 1]
      ),
      (fun (s1, e1, s2, e2) -> (s2 - s1) * (e2 - e1)),
      (fun (s1, e1, s2, e2) -> acc.[s1].[e1] + acc.[s2].[e2] - acc.[s1].[e2] - acc.[s2].[e1]),
      [| 0 |]
    )
    |> fun (_, area, landCost, accArea) ->
      seq {
        for s1 in 0..h - 1 do
        for e1 in 0..w - 1 do
        for s2 in h..(-1)..s1 + 1 do
        for e2 in w..(-1)..e1 + 1 -> (s1, e1, s2, e2)
      }
      |> Seq.iter (fun (s1, e1, s2, e2) ->
        area (s1, e1, s2, e2)
        |> fun curArea ->
          if curArea > accArea.[0] && int64 curArea * k + landCost (s1, e1, s2, e2) <= v
          then accArea.[0] <- curArea
      )
      |> fun () -> accArea.[0]
|> printfn "%d"

// https://atcoder.jp/contests/gigacode-2019/submissions/27166261
