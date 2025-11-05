// https://atcoder.jp/contests/joi2008ho/tasks/joi2008ho_d

let n, m = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let ks = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int)

let ks2 = ks |> Array.map ((Array.skip 1) >> (Array.chunkBySize 2) >> (Array.map (fun x -> x.[0], x.[1])))

let ss = Array.concat [ [| [| (0, 0) |] |]; ks2; [| [| (0, 0) |] |] ]

let infinity = System.Int32.MaxValue / 2

// i: 現在の行番号 (0: スタート，1 ~ n: 石のある行，n + 1: ゴール)
// j: i 行目にある j 番目の石のインデックス
// k: i 行目の j 番目の石に到達するまでに使った一行飛ばしジャンプの回数
let dp =
  Array.init (n + 2) (fun i ->
    Array.init (ss.[i].Length) (fun _ ->
      Array.create (m + 1) infinity
    )
  )

dp.[0].[0].[0] <- 0

let update i j k pi pk =
  if pi >= 0 && pk >= 0 then
    for pj in 0 .. ss.[pi].Length - 1 do
      if dp.[pi].[pj].[pk] <> infinity then
        let cx, cd = ss.[i].[j]
        let px, pd = ss.[pi].[pj]
        let cost = if pi = 0 || i = n + 1 then 0 else (pd + cd) * abs (px - cx)
        dp.[i].[j].[k] <- min dp.[i].[j].[k] (dp.[pi].[pj].[pk] + cost)

for i in 1 .. n + 1 do
  for j in 0 .. ss.[i].Length - 1 do
    for k in 0 .. m do
      update i j k (i - 1) k
      update i j k (i - 2) (k - 1)

dp.[n + 1].[0]
|> Array.min
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2008ho/submissions/70705638
