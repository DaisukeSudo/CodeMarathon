// https://atcoder.jp/contests/typical90/tasks/typical90_c

let rerooting =
  fun (identity: 'a)
    (operate: 'a -> 'a -> 'a)
    (operateNode: 'a -> int -> 'a)
    (nodeCount: int)
    (edges: (int * int) seq) ->

    let adjacents, indexes = (
      let a, b = Array.create nodeCount [], Array.create nodeCount []
      edges |> Seq.iter (fun (s, e) ->
          b.[s] <- a.[e].Length :: b.[s]
          b.[e] <- a.[s].Length :: b.[e]
          a.[s] <- e :: a.[s]
          a.[e] <- s :: a.[e]
      )
      (
        (a |> Array.map (List.rev >> List.toArray)),
        (b |> Array.map (List.rev >> List.toArray))
      )
    )

    let parents, order = (
      let parents = Array.create nodeCount -1
      let mutable order = []
      let mutable stack = [0]
      while stack |> List.length > 0 do
        match stack with
        | node :: ts ->
          stack <- ts
          order <- node :: order
          adjacents.[node]
          |> Array.filter ((<>) parents.[node])
          |> Array.iter (fun adjacent ->
            stack <- adjacent :: stack
            parents.[adjacent] <- node
          )
        | _ -> ()
      (parents, order |> List.rev)
    )

    let dp = Array.init nodeCount (fun i -> Array.create adjacents.[i].Length identity)
    let res = Array.create nodeCount identity

    order |> List.tail |> List.rev |> List.iter (fun node ->
      let parent = parents.[node]
      let acc =
        (identity, [|0..adjacents.[node].Length - 1|])
        ||> Array.fold (fun acc i ->
          if adjacents.[node].[i] = parent
          then acc
          else operate acc dp.[node].[i]
        )

      let parentIndex = adjacents.[node] |> Array.findIndex ((=) parent)
      let adjacentIndex = indexes.[node].[parentIndex]
      dp.[parent].[adjacentIndex] <- operateNode acc node
    )

    order |> List.iter (fun node ->
      let accRev =
        (identity, [|adjacents.[node].Length - 1..(-1)..1|])
        ||> Array.scan (fun acc i -> operate acc dp.[node].[i])
        |> Array.rev
      let acc =
        (identity, [|0..adjacents.[node].Length - 1|])
        ||> Array.fold (fun acc i ->
          dp.[adjacents.[node].[i]].[indexes.[node].[i]]
            <- operateNode (operate acc accRev.[i]) node
          operate acc dp.[node].[i]
        )

      res.[node] <- operateNode acc node
    )

    res

// ---- main ----

let n = stdin.ReadLine() |> int
let es = Array.init (n - 1) (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

(n, es) ||> rerooting 0 max (fun a _ -> a + 1) |> Seq.max
|> printfn "%d"

// https://atcoder.jp/contests/typical90/submissions/31468952
