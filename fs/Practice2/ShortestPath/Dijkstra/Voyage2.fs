// https://atcoder.jp/contests/joi2008yo/tasks/joi2008yo_f

// let stdin = new System.IO.StreamReader("2008-yo-t6-in4.txt")

(* priorityQueueBy *)
(
  fun p ->
    (new System.Collections.Generic.List<(int * int64)>())
    |> fun binaryHeap ->
      (
        (* enqueue *)
        (
          fun elem ->
            (
              [| binaryHeap.Count |],
              (binaryHeap.Add((elem, p elem)))
            )
            |> fun (x, _) ->
              Seq.initInfinite id
              |> Seq.takeWhile (fun _ -> x.[0] > 0)
              |> Seq.iter (
                fun _ ->
                  (
                    (x.[0]),
                    (x.[0] - 1) / 2
                  )
                  |> fun (i, j) ->
                    (binaryHeap.[i], binaryHeap.[j])
                    |> fun ((a, pa), (b, pb)) ->
                      if pa > pb
                      then
                        (
                          (binaryHeap.[i] <- (b, pb)),
                          (binaryHeap.[j] <- (a, pa))
                        )
                        |> fun _ -> x.[0] <- j
                      else
                        x.[0] <- 0
              )
        ),
        (* dequeue *)
        (
          fun () ->
            (binaryHeap.Count - 1)
            |> fun n ->
              (
                (binaryHeap.[0] |> fst),
                (binaryHeap.[0] <- binaryHeap.[n]),
                (binaryHeap.RemoveAt(n)),
                [| 0 |]
              )
              |> fun (head, _, _, x) ->
                Seq.initInfinite id
                |> Seq.map (fun _ -> (x.[0], (x.[0] * 2 + 1)))
                |> Seq.takeWhile (fun (_, j) -> j < n)
                |> Seq.iter (
                  fun (i, j) ->
                    (if j = n - 1 || (binaryHeap.[j] |> snd) > (binaryHeap.[j + 1] |> snd) then j else j + 1)
                    |> fun j ->
                      (binaryHeap.[i], binaryHeap.[j])
                      |> fun ((a, pa), (b, pb)) ->
                        if pa < pb
                        then
                          (
                            (binaryHeap.[i] <- (b, pb)),
                            (binaryHeap.[j] <- (a, pa))
                          )
                          |> fun _ -> x.[0] <- j
                        else
                          x.[0] <- n
                )
                |> fun _ -> head
        ),
        (* isEmpty *)
        (
          fun () -> binaryHeap.Count = 0
        )
      )
)
|> fun priorityQueueBy ->

  (***** main *****)

  stdin.ReadLine().Split()
  |> Array.map int
  |> fun x -> (x.[0], x.[1])
  |> fun (n, k) ->
    (
      Array.create (n + 1) Map.empty
    )
    |> fun fares ->
      (
        (
          fun a b ->
            (
              ((Array.create (n + 1) System.Int64.MaxValue) |> fun x -> (x.[a] <- 0L) |> fun _ -> x),
              Array.create (n + 1) false
            )
            |> fun (memo, finished) ->
              (
                priorityQueueBy (fun x -> memo.[x] * -1L)
              )
              |> fun (enqueue, dequeue, isEmpty) ->
                (
                  enqueue a
                )
                |> fun _ -> Seq.initInfinite ignore
                |> Seq.takeWhile (fun _ -> not (isEmpty () || finished.[b]))
                |> Seq.map dequeue
                |> Seq.filter (fun s -> not finished.[s])
                |> Seq.iter (fun s ->
                  fares.[s]
                  |> Map.iter (
                    fun k v ->
                      if memo.[k] > memo.[s] + v
                      then (memo.[k] <- memo.[s] + v) |> fun _ -> enqueue k
                  )
                  |> fun _ -> finished.[s] <- true
                )
                |> fun _ -> if memo.[b] = System.Int64.MaxValue then -1L else memo.[b]
        ),
        (
          fun c d e ->
            fares.[c]
            |> Map.tryFind d
            |> fun ox ->
              match ox with
              | Some(x) when e >= x -> ()
              | _ ->
                (
                  (fares.[c] <- fares.[c] |> Map.add d e),
                  (fares.[d] <- fares.[d] |> Map.add c e)
                )
                |> ignore
        )
      )
      |> fun (f0, f1) ->
        (
          [|[]|]
        )
        |> fun (results) ->
          [1..k]
          |> List.iter (fun _ ->
            stdin.ReadLine().Split()
            |> Array.map int
            |> fun x ->
              match x.[0] with
              | 0 -> (f0 x.[1] x.[2]) |> fun r -> results.[0] <- r :: results.[0]
              | _ -> (f1 x.[1] x.[2] (int64 x.[3]))
          )
          |> fun _ -> results.[0]
      |> Seq.rev
      |> Seq.map string
      |> String.concat "\n"
  |> printfn "%s"

// https://atcoder.jp/contests/joi2008yo/submissions/22350227
