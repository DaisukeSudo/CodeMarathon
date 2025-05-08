// https://atcoder.jp/contests/dwacon2018-prelims/tasks/dwacon2018_prelims_c

let p = 1_000_000_007L

let solve (killX: int[]) (killY: int[]) =
  let sumY = killY |> Array.sum
  let dp = Array.create (sumY + 1) 0L in dp.[0] <- 1L
  let mutable prev = -1
  let mutable acc = 0

  for i in 0 .. killX.Length - 1 do
    acc <- if prev = killX.[i] then acc + 1 else 1
    prev <- killX.[i]

    for j in acc .. sumY do
      dp.[j] <- (dp.[j] + dp.[j - acc]) % p

  dp.[sumY]

// ----

let _     = stdin.ReadLine()
let killA = stdin.ReadLine().Split() |> Array.map int
let killB = stdin.ReadLine().Split() |> Array.map int

(solve killA killB) * (solve killB killA) % p
|> stdout.WriteLine

// https://atcoder.jp/contests/dwacon2018-prelims/submissions/65579147
