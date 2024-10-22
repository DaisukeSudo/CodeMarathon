// https://atcoder.jp/contests/abc015/tasks/abc015_4

let w    = stdin.ReadLine() |> int
let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let ab   = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])

let dp0 = Array.init (k + 1) (fun _ -> Array.create (w + 1) 0)
(dp0, ab) ||> Array.fold (fun dp1 (a, b) ->
  let dp2 = Array.init (k + 1) (fun i -> Array.copy dp1.[i])
  for i in k - 1 .. -1 .. 0 do
    for j in a .. w do
      dp2.[i].[j] <- max dp2.[i].[j] (dp1.[i + 1].[j - a] + b)
  dp2
)
|> fun dp3 -> dp3.[0].[w]
|> stdout.WriteLine

// https://atcoder.jp/contests/abc015/submissions/59062792
