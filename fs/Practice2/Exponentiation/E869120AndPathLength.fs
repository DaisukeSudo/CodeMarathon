// https://atcoder.jp/contests/s8pc-1/tasks/s8pc_1_e

1_000_000_007L
|> fun p ->
  (
    ([| fun _ _ -> 0L |])
    |> fun fnc ->
      (
        fun m n ->
          match n with
          | _ when n = 0L -> 1L
          | _ when n % 2L = 0L -> fnc.[0] m (n / 2L) |> (fun x -> x * x % p)
          | _ -> m * fnc.[0] m (n - 1L) % p
      )
      |> fun fn -> (fnc.[0] <- fn) |> fun () -> fn
  )
  |> fun power ->
    (
      (stdin.ReadLine().Split() |> ignore),
      (stdin.ReadLine().Split() |> Array.map int64),
      (stdin.ReadLine().Split() |> Array.map int)
    )
    |> fun (_, a1, a2) ->
      Array.zeroCreate (a1.Length + 1)
      |> fun acc ->
        [ 1..a1.Length - 1 ]
        |> List.iter (fun i -> power a1.[i - 1] a1.[i] |> fun x -> acc.[i + 1] <- acc.[i] + x)
        |> fun () ->
          [a2; [| 1 |]]
          |> Array.concat
          |> Array.fold (
            fun (cost, i1) i2 -> (acc.[i1] - acc.[i2]) |> abs |> (+) cost |> fun x -> (x % p, i2)
          ) (0L, 1)
          |> fst
    |> printfn "%d"

// https://atcoder.jp/contests/s8pc-1/submissions/24935933
