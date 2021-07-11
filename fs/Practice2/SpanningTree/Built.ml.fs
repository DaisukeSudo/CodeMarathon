// https://atcoder.jp/contests/abc065/tasks/arc076_b

let readInput = fun () ->
  stdin.ReadLine()
  |> int
  |> fun n ->
    seq { 1..n }
    |> Seq.map(fun i ->
      stdin.ReadLine().Split()
      |> Array.map int
      |> fun x -> (i, x.[0], x.[1])
    )
    |> Seq.toArray

let makeEdges = fun (vs: (int * int * int) []) ->
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
  |> List.concat

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

let findRoot = fun (table: int []) ->
  (findTree table) >> (
    fun tree ->
      tree
      |> List.tail
      |> List.iter (fun x -> table.[x] <- tree |> Seq.head)
      |> fun () -> tree |> Seq.head
  )

let updateRoot = fun (table: int []) (newRoot: int) ->
  (findTree table) >> (
    fun tree ->
      tree
      |> Seq.head
      |> fun oldRoot ->
        tree
        |> List.iter (fun x -> table.[x] <- newRoot)
        |> fun () -> oldRoot
  )

let kruskal = fun (v: int, edges: seq<int * int * int>) ->
  [| 0..v |]
  |> fun table ->
    edges
    |> Seq.sortBy(fun (_, _, c) -> c)
    |> Seq.fold (fun cost (a, b, c) ->
      (findRoot table a)
      |> fun rA ->
        (updateRoot table rA b)
        |> fun rB ->
          if (rA <> rB)
          then cost + int64 c
          else cost
    ) 0L

()
|> readInput
|> fun vs -> (vs.Length, makeEdges vs)
|> kruskal
|> printfn "%d"

// https://atcoder.jp/contests/abc065/submissions/24159032
