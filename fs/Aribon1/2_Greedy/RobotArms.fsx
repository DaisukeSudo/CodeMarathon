// https://atcoder.jp/contests/keyence2020/tasks/keyence2020_b

stdin.ReadLine() |> int
|> fun n -> Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])
|> Array.map (fun (x, l) -> (x - l, x + l))
|> Array.sortBy snd
|> Seq.fold (fun (ans, lastE) (s, e) ->
  if s >= lastE then (ans + 1, e) else (ans, lastE)
) (0, System.Int32.MinValue)
|> fst
|> stdout.WriteLine

// https://atcoder.jp/contests/keyence2020/submissions/54266094
