// https://atcoder.jp/contests/typical90/tasks/typical90_bd

let n, s = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let a, b = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]) |> Array.unzip

let dp = Array.init (n + 1) (fun _ -> Array.create (s + 1) false) in dp.[0].[0] <- true

for i in [0 .. n - 1] do
  for j in [0 .. s] do
    if j >= a.[i] && dp.[i].[j - a.[i]] then dp.[i + 1].[j] <- true
    if j >= b.[i] && dp.[i].[j - b.[i]] then dp.[i + 1].[j] <- true

if not dp.[n].[s] then
  "Impossible" |> stdout.WriteLine
else
  let mutable ans = []
  let mutable pos = s

  for i in [n - 1 .. (-1) .. 0] do
    if pos >= b.[i] && dp.[i].[pos - b.[i]] then
      ans <- 'B' :: ans
      pos <- pos - b.[i]
    else
      ans <- 'A' :: ans
      pos <- pos - a.[i]

  ans |> List.map string |> System.String.Concat |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/42071905
