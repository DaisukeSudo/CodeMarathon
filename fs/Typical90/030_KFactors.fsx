// https://atcoder.jp/contests/typical90/tasks/typical90_ad

let n, k = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]

let memo = Array.create (n + 1) 0

seq { 2 .. n }
|> Seq.filter (fun i -> memo.[i] = 0) // prime numbers only
|> Seq.iter (fun i -> seq { i .. i .. n } |> Seq.iter (fun j ->
  memo.[j] <- memo.[j] + 1
))

memo
|> Seq.filter (fun x -> x >= k)
|> Seq.length
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/36342597
// https://atcoder.jp/contests/typical90/submissions/36342684
