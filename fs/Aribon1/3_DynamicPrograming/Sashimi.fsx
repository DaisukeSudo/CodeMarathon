// https://atcoder.jp/contests/kupc2012/tasks/kupc2012_10

let n  = stdin.ReadLine() |> int
let ws = stdin.ReadLine().Split() |> Array.map int64

let dp   = Array2D.init n n (fun i j -> if i = j then 0L else System.Int64.MaxValue)
let opt  = Array2D.init n n (fun i j -> if i = j then i else 0)
let psum = Array.zeroCreate (n + 1) in for i in 0 .. n - 1 do psum.[i + 1] <- psum.[i] + ws.[i]

for len in 2 .. n do
  for i in 0 .. n - len do
    let j = i + len - 1
    let v = psum.[j + 1] - psum.[i]
    for k in opt.[i, j - 1] .. min (opt.[i + 1, j]) (j - 1) do
      let cost = dp.[i, k] + dp.[k + 1, j] + v
      if cost < dp.[i, j] then
        dp.[i, j] <- cost
        opt.[i, j] <- k

dp.[0, n - 1]
|> stdout.WriteLine

// https://atcoder.jp/contests/kupc2012/submissions/68257364
