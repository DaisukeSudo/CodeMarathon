// https://atcoder.jp/contests/joi2015ho/tasks/joi2015ho_a

(
  (stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])),
  (stdin.ReadLine().Split() |> Array.map (int >> (+) -1))
)
|> fun ((n, _), path) ->
  (
    ([|0..n - 2|] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map int64 |> fun x -> (x.[0], x.[1], x.[2]))),
    (Array.create n 0L)
  )
  |> fun (fare, acc) ->
    path
    |> Array.reduce (fun s e ->
      (if s < e then (s, e) else (e, s))
      |> fun (a, b) ->
        [
          acc.[a] <- acc.[a] + 1L;
          acc.[b] <- acc.[b] - 1L
        ]
      |> fun _ -> e
    )
    |> fun _ ->
      [0..n - 2]
      |> List.iter (fun i -> acc.[i + 1] <- acc.[i + 1] + acc.[i])
    |> fun () ->
      [0..n - 2]
      |> List.fold (fun total i ->
        (fare.[i], acc.[i])
        |> fun ((a, b, c), cnt) -> min (a * cnt) (b * cnt + c)
        |> (+) total
      ) 0L
|> printfn "%d"

// https://atcoder.jp/contests/joi2015ho/submissions/27596355
