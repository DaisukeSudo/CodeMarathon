// https://atcoder.jp/contests/joi2008yo/tasks/joi2008yo_f

stdin.ReadLine().Split()
|> Array.map int
|> fun x -> (x.[0], x.[1])
|> fun (n, k) ->
  (
    Array.create (n + 1) Map.empty,
    [|[]|]
  )
  |> fun (fares: Map<int, int64>[], results) ->
    (
      (
        fun a b ->
          (
            Array.create (n + 1) System.Int64.MaxValue,
            Array.create (n + 1) false
          )
          |> fun (memo, finished) ->
            (
              (memo.[a] <- 0L),
              (finished.[a] <- true)
            )
            |> fun _ -> seq { 1..n }
            |> Seq.takeWhile(fun _ -> not finished.[b])
            |> Seq.fold(
              fun s _ ->
                fares.[s]
                |> Map.iter(
                  fun k v ->
                    if memo.[k] > memo.[s] + v
                    then memo.[k] <- memo.[s] + v
                )
                |> fun _ -> finished.[s] <- true
                |> fun _ -> [1..n]
                |> List.fold(
                  fun acc x ->
                    if not finished.[x] && memo.[x] < memo.[acc] then x else acc
                ) 0
                |> fun ns ->
                  if memo.[ns] = System.Int64.MaxValue
                  then (finished.[b] <- true)
                  |> fun _ -> ns
            ) a
            |> fun _ -> if memo.[b] = System.Int64.MaxValue then -1L else memo.[b]
            |> fun result -> results.[0] <- result :: results.[0]
      ),
      (
        fun c d e ->
          fares.[c]
          |> Map.tryFind d
          |> fun ox ->
            match ox with
            | Some(v) when e >= v -> ()
            | _ ->
              (
                (fares.[c] <- fares.[c] |> Map.add d e),
                (fares.[d] <- fares.[d] |> Map.add c e)
              )
              |> ignore
      )
    )
    |> fun (f0, f1) ->
      [1..k]
      |> List.iter(fun _ ->
        stdin.ReadLine().Split()
        |> Array.map int
        |> fun x ->
          match x.[0] with
          | 0 -> f0 x.[1] x.[2]
          | _ -> f1 x.[1] x.[2] (int64 x.[3])
      )
    |> fun _ -> results.[0]
    |> Seq.rev
    |> Seq.map string
    |> String.concat "\n"
|> printfn "%s"

// https://atcoder.jp/contests/joi2008yo/submissions/22262584
