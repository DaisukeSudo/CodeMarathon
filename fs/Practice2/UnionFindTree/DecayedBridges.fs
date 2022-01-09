// https://atcoder.jp/contests/abc120/tasks/abc120_d

(
  fun n ->
    ([| 0..n - 1 |])
    |> fun table ->
      (
        fun x -> ([| true |]) |> fun next -> Seq.initInfinite ignore |> Seq.takeWhile (fun () -> next.[0]) |> Seq.scan (fun c _ -> table.[c] |> fun p -> if p = c then (next.[0] <- false) |> fun _ -> c else p) x |> Seq.rev |> Seq.toList
      )
      |> fun findTree ->
        (
          findTree >> (fun tree -> tree |> List.tail |> List.iter (fun x -> table.[x] <- tree |> Seq.head) |> fun () -> tree |> Seq.head),
          fun newRoot -> findTree >> (fun tree -> tree |> Seq.head |> fun oldRoot -> tree |> List.iter (fun x -> table.[x] <- newRoot) |> fun () -> oldRoot)
        )
        |> fun (findRoot, updateRoot) ->
          (
            fun a b -> (findRoot a) |> fun rA -> (updateRoot rA b) |> fun rB -> (rA, rB)
          )
)
|> fun unionFindTree ->
  (* main *)
  stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])
  |> fun (n, m) ->
    (
      unionFindTree n,
      Array.create n 1L,
      [| int64 n *  (int64 n - 1L) / 2L |]
    )
    |> fun (unite, cs, ret) ->
      [1..m]
      |> List.map (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> (+) -1) |> fun x -> (x.[0], x.[1]))
      |> List.rev
      |> List.map (fun (a, b) ->
        (
          ret.[0],
          (unite a b)
          |> fun (rA, rB) ->
            if (rA <> rB) then
              [
                ret.[0] <- ret.[0] - cs.[rA] * cs.[rB];
                cs.[rA] <- cs.[rA] + cs.[rB];
                cs.[rB] <- 0L;
              ]
              |> ignore
        )
        |> fst
      )
      |> List.rev
      |> List.iter (printfn "%d") 

// https://atcoder.jp/contests/abc120/submissions/28429992
