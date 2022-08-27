// https://atcoder.jp/contests/typical90/tasks/typical90_t

let a, b, c = stdin.ReadLine().Split() |> fun x -> int64 x.[0], int x.[1], int64 x.[2]

let pow x n = (1L, [1..n]) ||> List.fold (fun a _ -> a * x)

a < pow c b
|> fun x -> if x then "Yes" else "No"
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/34379151
