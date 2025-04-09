// https://yukicoder.me/problems/no/269

let n, s, k = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1], x.[2]

let p = 1_000_000_007L
let inline modp x = (x + p) % p

let solve s =
  // dp[i, j] = i 人目までで合計 j 円になる方法の数
  let dp = Array2D.create (n + 1) (s + 1) 0L in dp.[0, 0] <- 1L
  [1 .. n] |> List.iter (fun i ->
    let a = i * k
    [0 .. s] |> List.iter (fun j ->
      dp.[i, j] <- (dp.[i, j] + dp.[i - 1, j]) |> modp
      if j >= a then
        dp.[i, j] <- (dp.[i, j] + dp.[i, j - a]) |> modp
    )
  )
  dp.[n, s]

let minTotal = (n - 1) * n / 2 * k
if s < minTotal then 0L else solve (s - minTotal)
|> stdout.WriteLine

// in: 3 10 1
// dp: [
//   [1; 0; 0; 0; 0; 0; 0; 0]
//   [1; 1; 1; 1; 1; 1; 1; 1]
//   [1; 1; 2; 2; 3; 3; 4; 4]
//   [1; 1; 2; 3; 4; 5; 7; 8]
// ]
// out: 8
