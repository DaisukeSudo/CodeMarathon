// https://atcoder.jp/contests/maximum-cup-2018/tasks/maximum_cup_2018_d

// n: 燃料タンクの数
// m: 環状道路の休憩所の総数
// l: 停まりたい休憩所の番号
// x: 最大周回数
// a: 各燃料タンクの補充可能な燃料量
let n, m, l, x = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1], x.[2], x.[3]
let a          = stdin.ReadLine().Split() |> Array.map int

// dp[i][j]: i 個の燃料タンクを使って 休憩所 j に到達する最小の周回数
let dp = Array.init (n + 1) (fun _ -> Array.create (m + 1) System.Int32.MaxValue) in
  dp.[0].[0] <- 1 // スタート地点 (休憩所 0) は燃料タンクを使わずに周回数 1 (スタート時点)

for i in 0 .. n - 1 do
  for j in 0 .. m - 1 do
    if dp.[i].[j] < System.Int32.MaxValue then // 未到達は除外
      // case 1: 燃料タンクを使用する＆周回する
      if j + a.[i] >= m then
        let f = a.[i] - (m - j) // 燃料量 - 最初の周回で到達する休憩所の数
        let d = 1 + (f / m)     // 最低周回数１ + 残りの燃料での周回数
        let j2 = f % m          // 残りの燃料での到達する休憩所の位置
        dp.[i + 1].[j2] <- min dp.[i + 1].[j2] (dp.[i].[j] + d)
      // case 2: 燃料タンクを使用する＆周回しない
      else
        let j2 = j + a.[i]
        dp.[i + 1].[j2] <- min dp.[i + 1].[j2] dp.[i].[j]
      // case 3: 燃料タンクを使用しない
      dp.[i + 1].[j] <- min dp.[i + 1].[j] dp.[i].[j]

// 休憩所 l に到達する最小周回数が x 以下なら "Yes"
if dp.[n].[l] <= x then "Yes" else "No"
|> stdout.WriteLine

// https://atcoder.jp/contests/maximum-cup-2018/submissions/60646874
