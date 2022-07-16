// https://atcoder.jp/contests/typical90/tasks/typical90_p

let n = stdin.ReadLine() |> int
let a, b, c = stdin.ReadLine().Split() |> Array.map int |> Array.sortDescending |> fun x -> x.[0], x.[1], x.[2]

seq {0..n / a} |> Seq.collect (fun i ->
  let n1 = n - a * i
  seq {0..n1 / b} |> Seq.map (fun j ->
    let n2 = n1 - b * j
    if n2 % c = 0 then Some(i + j + n2 / c) else None
  )
)
|> Seq.filter (Option.isSome)
|> Seq.map (Option.get)
|> Seq.min
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/33254850
