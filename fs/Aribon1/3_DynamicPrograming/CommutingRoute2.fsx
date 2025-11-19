// https://atcoder.jp/contests/joi2010yo/tasks/joi2010yo_e

let MOD = 100000

let w, h = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]

let dp = Array3D.zeroCreate (h + 1) (w + 1) 2
for i in 1 .. h do dp.[i, 0, 0] <- 1
for j in 1 .. w do dp.[0, j, 1] <- 1

for i in 1 .. h do
  for j in 1 .. w do
    dp.[i, j, 0] <- (
      dp.[i - 1, j, 0] +
      if i >= 2 then dp.[i - 2, j, 1] else 0
    ) % MOD
    dp.[i, j, 1] <- (
      dp.[i, j - 1, 1] +
      if j >= 2 then dp.[i, j - 2, 0] else 0
    ) % MOD

(
  dp.[h - 1, w, 1] + 
  dp.[h, w - 1, 0]
) % MOD
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2010yo/submissions/71074136
