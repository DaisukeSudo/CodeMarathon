let priorityQueue<'a when 'a : comparison> =
  fun () ->
    (new System.Collections.Generic.List<'a>())
    |> fun binaryHeap ->
      (
        (* enqueue *)
        (
          fun elem ->
            (
              [| binaryHeap.Count |],
              (binaryHeap.Add(elem))
            )
            |> fun (x, _) ->
              Seq.initInfinite ignore
              |> Seq.takeWhile (fun _ -> x.[0] > 0)
              |> Seq.iter (
                fun _ ->
                  (
                    (x.[0]),
                    (x.[0] - 1) / 2
                  )
                  |> fun (i, j) ->
                    (binaryHeap.[i], binaryHeap.[j])
                    |> fun (a, b) ->
                      if a > b
                      then
                        (
                          (binaryHeap.[i] <- b),
                          (binaryHeap.[j] <- a)
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
                (binaryHeap.[0]),
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
                    (if j = n - 1 || binaryHeap.[j] > binaryHeap.[j + 1] then j else j + 1)
                    |> fun j ->
                      (binaryHeap.[i], binaryHeap.[j])
                      |> fun (a, b) ->
                        if a < b
                        then
                          (
                            (binaryHeap.[i] <- b),
                            (binaryHeap.[j] <- a)
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
        ),
        (* queue *)
        (
          fun () -> binaryHeap |> List.ofSeq
        )
      )

priorityQueue()
|> fun (enqueue, dequeue, isEmpty, queue) ->
  [1..9]
  |> Seq.iter (
    fun i ->
      printfn "enqueue %d" i
      enqueue i
      queue ()    |> printfn "%A"
  )
  queue ()
  |> Seq.iter (
    fun _ ->
      dequeue ()  |> printfn "dequeue %d"
      queue ()    |> printfn "%A"
  )

// enqueue 1
// [1]
// enqueue 2
// [2; 1]
// enqueue 3
// [3; 1; 2]
// enqueue 4
// [4; 3; 2; 1]
// enqueue 5
// [5; 4; 2; 1; 3]
// enqueue 6
// [6; 4; 5; 1; 3; 2]
// enqueue 7
// [7; 4; 6; 1; 3; 2; 5]
// enqueue 8
// [8; 7; 6; 4; 3; 2; 5; 1]
// enqueue 9
// [9; 8; 6; 7; 3; 2; 5; 1; 4]
// dequeue 9
// [8; 7; 6; 4; 3; 2; 5; 1]
// dequeue 8
// [7; 4; 6; 1; 3; 2; 5]
// dequeue 7
// [6; 4; 5; 1; 3; 2]
// dequeue 6
// [5; 4; 2; 1; 3]
// dequeue 5
// [4; 3; 2; 1]
// dequeue 4
// [3; 1; 2]
// dequeue 3
// [2; 1]
// dequeue 2
// [1]
// dequeue 1
// []
