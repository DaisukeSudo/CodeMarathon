// https://atcoder.jp/contests/typical90/tasks/typical90_az

Array.init (stdin.ReadLine() |> int) (fun _ ->
  stdin.ReadLine().Split() |> Array.sumBy int64
)
|> Array.reduce (fun a b -> a * b % 1_000_000_007L)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/41286063
