// https://atcoder.jp/contests/tdpc/tasks/tdpc_knapsack

let n0, w0, c0 = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1], x.[2]
let wvc = Array.init n0 (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int64 x.[1], int x.[2])

// dp1.[u].[w] : 前回の色まででの u 色，重さ w の最大価値
// dp2.[u].[w] : 現在の色を含めた u 色，重さ w の最大価値（この色を必ず 1 つ以上使う）
let dp1 = Array.init (c0 + 1) (fun _ -> Array.create (w0 + 1) -1L)
let dp2 = Array.init (c0 + 1) (fun _ -> Array.create (w0 + 1) -1L)

dp1.[0].[0] <- 0L

wvc
|> Array.groupBy (fun (_, _, c) -> c)
|> Array.iter (fun (_, items) ->
  for row in dp2 do Array.fill row 0 (w0 + 1) -1L

  for (wi, vi, _) in items do
    for u = 1 to c0 do
      for w = w0 downto wi do
        let prevw0 = w - wi
        
        // この色を継続して使う (dp2 -> dp2)
        if dp2.[u].[prevw0] <> -1L then
          let x = dp2.[u].[prevw0] + vi
          if x > dp2.[u].[w] then dp2.[u].[w] <- x
        
        // この色を新規に使い始める (dp1 -> dp2)
        if dp1.[u - 1].[prevw0] <> -1L then
          let x = dp1.[u - 1].[prevw0] + vi
          if x > dp2.[u].[w] then dp2.[u].[w] <- x

  // マージ (dp2 -> dp1)
  for u = 1 to c0 do
    for w = 0 to w0 do
      if dp2.[u].[w] > dp1.[u].[w] then dp1.[u].[w] <- dp2.[u].[w]
)

let mutable ans = 0L in
  for u = 0 to c0 do
    for w = 0 to w0 do
      if dp1.[u].[w] > ans then ans <- dp1.[u].[w]

ans |> stdout.WriteLine

// https://atcoder.jp/contests/tdpc/submissions/71612702
