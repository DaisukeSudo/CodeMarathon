// Omnidirectional Tree DP

let rerooting =
  fun (identity: 'a)
      (operate: 'a -> 'a -> 'a)
      (operateNode: 'a -> int -> 'a)
      (nodeCount: int)
      (edges: (int * int) seq) ->

    if nodeCount < 3 then Array.init nodeCount (operateNode identity) else (

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

      // printfn "adjacents: %A" adjacents
      // printfn "indexes: %A" indexes

      // init ordered tree
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

      // printfn "parents: %A" parents
      // printfn "order: %A" order

      let dp = Array.init nodeCount (fun i -> Array.create adjacents.[i].Length identity)
      let res = Array.create nodeCount identity

      // from leaf
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

      // printfn "dp1: %A" dp

      // to leaf

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

      // printfn "dp2: %A" dp

      res
    )

    // // query
    // |> fun res -> fun node -> res.[node]

let n  = 5
let es = [(0, 1); (1, 2); (1, 3); (3, 4)]

(n, es) ||> rerooting 0 max (fun a _ -> a + 1)
|> printfn "%A"

// adjacents: [|[|1|]; [|0; 2; 3|]; [|1|]; [|1; 4|]; [|3|]|]
// indexes: [|[|0|]; [|0; 0; 0|]; [|1|]; [|2; 0|]; [|1|]|]
// parents: [|-1; 0; 1; 1; 3|]
// order: [0; 1; 3; 4; 2]
// dp1: [|[|3|]; [|0; 1; 2|]; [|0|]; [|0; 1|]; [|0|]|]
// dp2: [|[|3|]; [|1; 1; 2|]; [|3|]; [|2; 1|]; [|3|]|]
// [|4; 3; 4; 3; 4|]
