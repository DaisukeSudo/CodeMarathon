// https://atcoder.jp/contests/joi2009ho/tasks/joi2009ho_d

let h, w, n = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int x.[2]
let a = Array.init h (fun _ -> stdin.ReadLine().Split() |> Array.map int)

let dp = Array2D.zeroCreate (h + 1) (w + 1)
dp.[0, 0] <- n - 1

for i in 0 .. h - 1 do
  for j in 0 .. w - 1 do
    let half = dp.[i, j] / 2
    let rem  = dp.[i, j] % 2
    dp.[i + 1, j] <- dp.[i + 1, j] + half + if a.[i].[j] = 0 then rem else 0
    dp.[i, j + 1] <- dp.[i, j + 1] + half + if a.[i].[j] = 1 then rem else 0

let mutable r, c = 0, 0

while r < h && c < w do
  let d = (a.[r].[c] ^^^ dp.[r, c]) &&& 1
  r <- r + if d = 0 then 1 else 0
  c <- c + if d = 1 then 1 else 0

printfn "%d %d" (r + 1) (c + 1)

// https://atcoder.jp/contests/joi2009ho/submissions/70896759
