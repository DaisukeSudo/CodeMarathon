// https://atcoder.jp/contests/joisc2010/tasks/joisc2010_finals

stdin.ReadLine().Split()
|> Array.map int
|> fun x -> (x.[0], x.[1], x.[2])
|> fun (n, m, k) ->
  ([| 0..n |])
  |> fun ps ->
    (
      [| n |],
      seq { 1..m } |> Seq.map(fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1], x.[2])),
      k,
      (
        fun x ->
          ([| true |])
          |> fun next ->
            Seq.initInfinite ignore
            |> Seq.takeWhile (fun () -> next.[0])
            |> Seq.scan (fun c _ -> ps.[c] |> fun p -> if p = c then (next.[0] <- false) |> fun _ -> c else p) x
            |> Seq.rev
            |> Seq.toList
            |> fun tree -> match tree with | r::cs -> (r, cs) | _ -> (x, [])
            |> fun (r, cs) -> cs |> List.iter (fun c -> ps.[c] <- r) |> fun () -> r
      ),
      (
        fun x r ->
          ([| true |])
          |> fun next ->
            Seq.initInfinite ignore
            |> Seq.takeWhile (fun () -> next.[0])
            |> Seq.fold (fun c _ -> ps.[c] |> fun p -> (ps.[c] <- r) |> fun _ -> if p = c then (next.[0] <- false) |> fun _ -> c else p) x
      )
    )
  |> fun (cnt, edges, k, findTree, updateRoot) ->
    edges
    |> Seq.sortBy(fun (_, _, c) -> c)
    |> Seq.takeWhile (fun _ -> cnt.[0] > k)
    |> Seq.fold (fun cost (a, b, c) ->
      (findTree a)
      |> fun rA ->
        (updateRoot b rA)
        |> fun rB ->
          if (rA <> rB)
          then (cnt.[0] <- cnt.[0] - 1) |> fun _ -> cost + int64 c
          else cost
    ) 0L
|> printfn "%d"

// https://atcoder.jp/contests/joisc2010/submissions/23730268
