// https://atcoder.jp/contests/typical90/tasks/typical90_ci

let n, p, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int64 x.[1], int x.[2]
let a = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int64)

let count x =
  let dist = a |> Array.map (Array.map (fun y -> if y = -1L then x else y))

  for k in 0 .. n - 1 do
    for i in 0 .. n - 1 do
      for j in 0 .. n - 1 do
        dist.[i].[j] <- min dist.[i].[j] (dist.[i].[k] + dist.[k].[j])

  let mutable ans = 0
  for i in 0 .. n - 1 do
    for j in i + 1 .. n - 1 do
      if dist.[i].[j] <= p then ans <- ans + 1
  ans

let inf = 1_000_000_000L

let search condition =
  let mutable l = 0L
  let mutable r = inf
  while l + 1L < r do
    let m = (l + r) / 2L
    if condition m then
      r <- m
    else
      l <- m
  r

let l = search (count >> (>=) k)
let r = search (count >> (>) k)
if l = inf then "0"
else if r = inf then "Infinity"
else string (r - l)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/50282725
