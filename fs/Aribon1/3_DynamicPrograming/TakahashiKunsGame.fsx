// https://atcoder.jp/contests/arc057/tasks/arc057_b

let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int64 x.[1]
let a    = Array.init n (fun _ -> stdin.ReadLine() |> int64)

let cs = ((0L, []), a) ||> Array.fold (fun (acc, sums) x -> (acc + x, acc + x :: sums)) |> snd |> List.rev |> List.toArray // cumulative sum

let dp = Array.init (n + 1) (fun i -> if i = 0 then 0L elif i = 1 then 1L else System.Int64.MaxValue)

for i in 0 .. n - 2 do
  for j in i + 2 .. -1 .. 1 do
    dp.[j] <- min dp.[j] (dp.[j - 1] + dp.[j - 1] * a.[i + 1] / cs.[i] + 1L)

if cs |> Seq.last |> ((=) k) then 1
else ([0 .. n] |> Seq.filter (fun i -> dp.[i] <= k) |> Seq.max)
|> stdout.WriteLine

// https://atcoder.jp/contests/arc057/submissions/60227740
