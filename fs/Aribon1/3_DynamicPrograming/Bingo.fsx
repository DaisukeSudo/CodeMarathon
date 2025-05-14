// https://atcoder.jp/contests/joi2009yo/tasks/joi2009yo_f

// n0: Card size (n0 * n0 = Number of cells)
// m0: Maximum value of one cell
// s0: Total value of all cells
let n0, m0, s0 = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1], x.[2]

let n  = n0 * n0          // Number of cells
let m1 = n * (n + 1) / 2  // Minimum value of all cells
let s  = s0 - m1          // Number to be distributed
let m  = m0 - n           // The limit for adding to each cell

let p = 100000
let add b a = (a + b) % p
let sub b a = (a - b + p) % p

// dp[i][j]: The number of ways to make the sum j using i numbers
let dp = Array.init (n + 1) (fun _ -> Array.create (s + 1) 0)
dp.[0].[0] <- 1

for i in 1 .. n do
  for j in 0 .. s do
    let v1 = dp.[i - 1].[j]
    let v2 = if j >= i     then dp.[i].[j - i]         else 0
    let v3 = if j >= i + m then dp.[i - 1].[j - i - m] else 0
    dp.[i].[j] <- dp.[i].[j] |> add v1 |> add v2 |> sub v3

dp.[n].[s] |> stdout.WriteLine

// https://atcoder.jp/contests/joi2009yo/submissions/65790622
