// https://atcoder.jp/contests/typical90/tasks/typical90_p

let n = stdin.ReadLine() |> int
let a, b, c = stdin.ReadLine().Split() |> Array.map int |> Array.sortDescending |> fun x -> x.[0], x.[1], x.[2]

seq {n / a..(-1)..0} |> Seq.map (fun i ->
  seq {(n - a * i) / b..(-1)..0}
  |> Seq.tryFind (fun j -> (n - a * i - b * j) % c = 0)
  |> Option.map (fun j -> i + j + (n - a * i - b * j) / c)
)
|> Seq.tryFind(Option.isSome) |> Option.get |> Option.get
|> stdout.WriteLine
