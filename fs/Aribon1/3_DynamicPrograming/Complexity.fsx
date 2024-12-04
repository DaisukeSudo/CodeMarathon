// https://atcoder.jp/contests/agc033/tasks/agc033_d

let h, w = stdin.ReadLine().Split() |> Array.map int |> fun arr -> arr.[0], arr.[1]
let a    = [| for _ in 1 .. h -> stdin.ReadLine().Trim() |]

// 指定した領域が単色かどうか
let isMonochrome =
  let cs = Array2D.create (h + 1) (w + 1) 0
  for i in 0 .. h - 1 do
    for j in 0 .. w - 1 do
      cs.[i + 1, j + 1] <-
          cs.[i, j + 1]       // 現在の行の前列までの黒マス数
        + cs.[i + 1, j]       // 現在の列の前行までの黒マス数
        - cs.[i, j]           // 二重カウントされる部分を補正
        + if a.[i].[j] = '#' then 1 else 0  // 現在のマスが黒なら 1 それ以外なら 0

  fun r1 c1 r2 c2 ->
    let totalBlack = 
          cs.[r2 + 1, c2 + 1] // (0, 0) から (r2, c2) までの黒マス数
        - cs.[r2 + 1, c1]     // 左側 (r2, 0) から (r2, c1 - 1) 部分を除去
        - cs.[r1, c2 + 1]     // 上側 (0, c2) から (r1 - 1, c2) 部分を除去
        + cs.[r1, c1]         // 左上 (0, 0) から (r1 - 1, c1 - 1) 部分を再加算（2回引いたため）
    let totalCells = (r2 - r1 + 1) * (c2 - c1 + 1)
    totalBlack = 0 || totalBlack = totalCells // 全て白または全て黒であれば単色

// 長方形領域 (r1, c1) から (r2, c2) の複雑さ
let dp = Array4D.create h w h w System.Int32.MaxValue

// 小さい領域→大きい領域 の順で更新
seq {
  for y in 0 .. h - 1 do        // 縦幅 y
  for x in 0 .. w - 1 do        // 横幅 x
  for r1 in 0 .. h - y - 1 do   // 左上の行位置
  for c1 in 0 .. w - x - 1 ->   // 左上の列位置
    (r1, c1, r1 + y, c1 + x)    // 領域 (r1, c1) から (r2, c2)
}
|> Seq.iter (fun (r1, c1, r2, c2) ->
  if isMonochrome r1 c1 r2 c2 then
    dp.[r1, c1, r2, c2] <- 0
  else
    // 横に分割: 分割位置 m を指定して [r1, m] と [m + 1, r2] の最大値にて更新
    for m in r1 .. r2 - 1 do
      dp.[r1, c1, r2, c2] <- 
        min dp.[r1, c1, r2, c2] 
          (max dp.[r1, c1, m, c2] dp.[m + 1, c1, r2, c2] + 1)
    // 縦に分割: 分割位置 m を指定して [c1, m] と [m + 1, c2] の最大値にて更新
    for m in c1 .. c2 - 1 do
      dp.[r1, c1, r2, c2] <- 
        min dp.[r1, c1, r2, c2] 
          (max dp.[r1, c1, r2, m] dp.[r1, m + 1, r2, c2] + 1)
)

dp.[0, 0, h - 1, w - 1]
|> stdout.WriteLine

// https://atcoder.jp/contests/agc033/submissions/60428567
