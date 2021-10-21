// https://atcoder.jp/contests/joi2010ho/tasks/joi2010ho_a

100_000L
|> fun p ->
  stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])
  |> fun (n, m) ->
    (
      ([1..n - 1] |> List.map (fun _ -> stdin.ReadLine() |> int64)),
      ([1..m]     |> List.map (fun _ -> stdin.ReadLine() |> int))
    )
    |> fun (ns, ms) ->
      ns
      |> Seq.fold (fun (h::ts) x -> (x + h)::h::ts) [0L]
      |> List.rev
      |> List.toArray
      |> fun acc ->
        ms
        |> Seq.fold (
          fun (i, cost) d ->
            (
              i + d,
              (cost + abs (acc.[i] - acc.[i + d])) % p
            )
        ) (0, 0L)
        |> snd
|> printfn "%d"

// ----

// [|0; 2; 3; 4; 7; 9; 10|]
// [(0, 0); (2, 3); (1, 4); (4, 9); (6, 12); (3, 18)]

// https://atcoder.jp/contests/joi2010ho/submissions/26714028
