// https://atcoder.jp/contests/arc085/tasks/arc085_b

let n, _z, w = stdin.ReadLine().Split() |> fun x -> int x.[0], int64 x.[1], int64 x.[2]
let a        = stdin.ReadLine().Split() |> Array.map int64

(
  if n = 1 then
    let x = a.[n - 1]
    abs (x - w)
  else
    let x = a.[n - 1]
    let y = a.[n - 2]
    max (abs (x - y)) (abs (x - w))
)
|> stdout.WriteLine

// https://atcoder.jp/contests/arc085/submissions/67653341
