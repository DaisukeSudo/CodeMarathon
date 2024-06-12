// https://atcoder.jp/contests/abc103/tasks/abc103_d

stdin.ReadLine().Split() |> fun x -> int x.[1]
|> fun m -> Array.init m (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1] - 1)
|> Array.sortBy snd
|> Seq.fold (fun (ans, lastE) (s, e) ->
  if s > lastE then (ans + 1, e) else (ans, lastE)
) (0, System.Int32.MinValue)
|> fst
|> stdout.WriteLine

// https://atcoder.jp/contests/abc103/submissions/54468890
