// https://atcoder.jp/contests/typical90/tasks/typical90_ax

stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
|> fun (n, l) ->
  (Array.create (n + 1) 1L, [l .. n])
  ||> List.fold (fun dp i ->
    dp.[i] <- (dp.[i - 1] + dp.[i - l]) % 1_000_000_007L; dp
  )
|> Array.last
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/40574698
