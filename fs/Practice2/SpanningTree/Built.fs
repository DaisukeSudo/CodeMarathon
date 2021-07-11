// https://atcoder.jp/contests/abc065/tasks/arc076_b

(
  fun n ->
    ([| n - 1 |], [| 0..n |])
    |> fun (cnt, table) ->
      (
        (* findTree *)
        fun x ->
          ([| true |])
          |> fun next ->
            Seq.initInfinite ignore
            |> Seq.takeWhile (fun () -> next.[0])
            |> Seq.scan (
              fun current _ ->
                table.[current]
                |> fun parent ->
                  if parent = current
                  then (next.[0] <- false) |> fun _ -> current
                  else parent
            ) x
            |> Seq.rev
            |> Seq.toList
      )
      |> fun findTree ->
        (
          (* findRoot *)
          findTree >> (
            fun tree ->
              tree
              |> List.tail
              |> List.iter (fun x -> table.[x] <- tree |> Seq.head)
              |> fun () -> tree |> Seq.head
          ),
          (* updateRoot *)
          fun newRoot ->
            findTree
            >> (
              fun tree ->
                tree
                |> Seq.head
                |> fun oldRoot ->
                  tree
                  |> List.iter (fun x -> table.[x] <- newRoot)
                  |> fun () -> oldRoot
            )
        )
        |> fun (findRoot, updateRoot) ->
          (
            (* unite *)
            (
              fun a b ->
                (findRoot a)
                |> fun rA ->
                  (updateRoot rA b)
                  |> fun rB ->
                    if (rA <> rB)
                    then (cnt.[0] <- cnt.[0] - 1) |> fun _ -> true
                    else false
            ),
            (* count *)
            (
              fun () -> cnt.[0]
            )
          )
)
|> fun unionTree ->

  (* main *)
  stdin.ReadLine()
  |> int
  |> fun n ->
    (
      seq { 1..n }
      |> Seq.map(fun i ->
        stdin.ReadLine().Split()
        |> Array.map int
        |> fun x -> (i, x.[0], x.[1])
      )
      |> Seq.toArray,
      (unionTree n)
    )
    |> fun (vs, (unite, count)) ->
      [
        (
          vs
          |> Array.sortWith (fun (_, x1, y1) (_, x2, y2) -> if x1 <> x2 then x1 - x2 else y1 - y2)
          |> fun vsX ->
            [1..vs.Length - 1]
            |> List.map (fun i -> (vsX.[i - 1], vsX.[i]) |> fun ((i1, x1, _), (i2, x2, _)) -> (i1, i2, x2 - x1))
        );
        (
          vs
          |> Array.sortWith (fun (_, x1, y1) (_, x2, y2) -> if y1 <> y2 then y1 - y2 else x1 - x2)
          |> fun vsY ->
            [1..vs.Length - 1]
            |> List.map (fun i -> (vsY.[i - 1], vsY.[i]) |> fun ((i1, _, y1), (i2, _, y2)) -> (i1, i2, y2 - y1))
        );
      ]
      |> Seq.concat
      |> Seq.sortBy(fun (_, _, c) -> c)
      |> Seq.takeWhile (fun _ -> count () > 0)
      |> Seq.fold (fun cost (a, b, c) -> if unite a b then cost + int64 c else cost) 0L
  |> printfn "%d"

// https://atcoder.jp/contests/abc065/submissions/24161118
