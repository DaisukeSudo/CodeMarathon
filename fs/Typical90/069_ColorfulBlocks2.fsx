// https://atcoder.jp/contests/typical90/tasks/typical90_bq

let p = 1000000007L

let binPow _a _b =
  let mutable a = _a
  let mutable b = _b
  let mutable ans = 1L
  while b <> 0L do
    if b % 2L = 1L then
      ans <- ans * a % p
    a <- a * a % p
    b <- b / 2L
  ans

// ----

let n, k = stdin.ReadLine().Split() |> fun x -> int64 x.[0], int64 x.[1]

if n = 1L then k else k * (k - 1L) % p * binPow (k - 2L) (n - 2L) % p
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/45518942
