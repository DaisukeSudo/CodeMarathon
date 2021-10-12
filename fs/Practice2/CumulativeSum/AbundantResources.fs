// https://atcoder.jp/contests/nikkei2019-final/tasks/nikkei2019_final_a

(
  stdin.ReadLine() |> int,
  stdin.ReadLine().Split() |> Array.map int64
)
|> fun (n, ks) ->
  ks
  |> Seq.fold (fun (h::ts) x -> (x + h)::h::ts) [0L]
  |> List.rev
  |> List.toArray
  |> fun acc ->
    [1..n]
    |> Seq.map (
      fun k ->
        [0..n - k]
        |> Seq.map (fun i -> acc.[i + k] - acc.[i])
        |> Seq.max
    )
|> Seq.iter (printfn "%d")

// https://atcoder.jp/contests/nikkei2019-final/submissions/26529980
