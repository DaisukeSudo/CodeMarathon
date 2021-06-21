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

let findRoot = fun (table: int []) (x: int) ->
  [| true |]
  |> fun next ->
    Seq.initInfinite ignore
    |> Seq.takeWhile (fun () -> next.[0])
    |> Seq.fold (
      fun current _ ->
        table.[current]
        |> fun parent ->
          if parent = current
          then (next.[0] <- false) |> fun _ -> current
          else parent
    ) x
    |> fun root -> (table.[x] <- root) |> fun () -> root

let kruskal = fun (v: int, edges: seq<int * int * int>, k: int) ->
  ([| v |], [| 0..v |])
  |> fun (cnt, table) ->
    edges
    |> Seq.sortBy(fun (_, _, c) -> c)
    |> Seq.takeWhile (fun _ -> cnt.[0] > k)
    |> Seq.fold (fun cost (a, b, c) ->
      (findRoot table a, findRoot table b)
      |> fun (rA, rB) ->
        if (rA <> rB)
        then
          ((table.[rB] <- rA), (cnt.[0] <- cnt.[0] - 1))
          |> fun _ -> cost + int64 c
        else cost
    ) 0L

()
|> readInput
|> kruskal
|> printfn "%d"

// https://atcoder.jp/contests/joisc2010/submissions/23663414
