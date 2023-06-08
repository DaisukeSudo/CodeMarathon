// https://atcoder.jp/contests/typical90/tasks/typical90_bc

let n, p, q = stdin.ReadLine().Split() |> fun x -> int x.[0], int64 x.[1], int64 x.[2]
let a = stdin.ReadLine().Split() |> Array.map int64

let mutable ans = 0
for i in [4 .. n - 1] do
  for j in [3 .. i - 1] do
    for k in [2 .. j - 1] do
      for l in [1 .. k - 1] do
        for m in [0 .. l - 1] do
          if a.[i] * a.[j] % p * a.[k] % p * a.[l] % p * a.[m] % p = q then
            ans <- ans + 1

ans |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/42071386
