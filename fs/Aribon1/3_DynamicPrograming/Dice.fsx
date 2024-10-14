// https://atcoder.jp/contests/tdpc/tasks/tdpc_dice

let n, d = stdin.ReadLine().Split() |> fun x -> int x.[0], int64 x.[1]

let rec gcd a b = if b = 0L then a else gcd b (a % b)

let dp0 = [(1L, 1.0)] |> Map.ofList
(dp0, [1 .. n])       ||> List.fold (fun dp1 _ ->
  (Map.empty, dp1)    ||> Map.fold  (fun dp2 g1 p1 ->
    (dp2, [1L .. 6L]) ||> List.fold (fun dp3 i ->
      let g2 = gcd (g1 * i) d
      let p2 = (dp3 |> Map.tryFind g2 |> Option.defaultValue 0.0) + (p1 / 6.0)
      dp3 |> Map.add g2 p2
    )
  )
) 
|> Map.tryFind d
|> Option.defaultValue 0.0
|> printfn "%.9f"

// https://atcoder.jp/contests/tdpc/submissions/58799014
