// https://atcoder.jp/contests/joisc2010/tasks/joisc2010_finals

let readInput = fun () ->
  stdin.ReadLine().Split()
  |> Array.map int
  |> fun x -> (x.[0], x.[1], x.[2])
  |> fun (n, m, k) ->
    (
      n,
      seq { 1..m }
      |> Seq.map(fun _ ->
        stdin.ReadLine().Split()
        |> Array.map int
        |> fun x -> (x.[0], x.[1], x.[2])
      ),
      k
    )

let findTree = fun (table: int []) (x: int) ->
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
    |> fun tree ->
      match tree with
      | root::items -> (root, items)
      | _ -> (x, [])
    |> fun (root, items) -> items |> List.iter (fun item -> table.[item] <- root) |> fun () -> root

let updateRoot = fun (table: int []) (x: int) (root: int) ->
  ([| true |])
  |> fun next ->
    Seq.initInfinite ignore
    |> Seq.takeWhile (fun () -> next.[0])
    |> Seq.fold (
      fun current _ ->
        table.[current]
        |> fun parent ->
          (table.[current] <- root)
          |> fun _ ->
            if parent = current
            then (next.[0] <- false) |> fun _ -> current
            else parent
    ) x

let kruskal = fun (v: int, edges: seq<int * int * int>, k: int) ->
  ([| v |], [| 0..v |])
  |> fun (cnt, table) ->
    edges
    |> Seq.sortBy(fun (_, _, c) -> c)
    |> Seq.takeWhile (fun _ -> cnt.[0] > k)
    |> Seq.fold (fun cost (a, b, c) ->
      (findTree table a)
      |> fun rA ->
        (updateRoot table b rA)
        |> fun rB ->
        if (rA <> rB)
        then (cnt.[0] <- cnt.[0] - 1) |> fun _ -> cost + int64 c
        else cost
    ) 0L

()
|> readInput
|> kruskal
|> printfn "%d"

// https://atcoder.jp/contests/joisc2010/submissions/23729904
