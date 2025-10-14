// https://atcoder.jp/contests/abc037/tasks/abc037_d

let MOD = 1_000_000_007

let h, w = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let a = Array.init h (fun _ -> stdin.ReadLine().Split() |> Array.map int)

let dp = Array2D.create h w 1

let cs =
  [|
    for i in 0 .. h - 1 do
    for j in 0 .. w - 1 do
      yield (i, j)
  |]
Array.sortInPlaceBy (fun (i, j) -> a.[i].[j]) cs

for (i, j) in cs do
  for (i2, j2) in [(i - 1, j); (i + 1, j); (i, j - 1); (i, j + 1)] do
    if i2 >= 0 && i2 < h && j2 >= 0 && j2 < w
      && a.[i].[j] < a.[i2].[j2] then
        dp.[i2, j2] <- (dp.[i2, j2] + dp.[i, j]) % MOD

let mutable ans = 0
for (i, j) in cs do
  ans <- (ans + dp.[i, j]) % MOD

ans |> stdout.WriteLine

// https://atcoder.jp/contests/abc037/submissions/70142499 TLE
// https://atcoder.jp/contests/abc037/submissions/70142713
