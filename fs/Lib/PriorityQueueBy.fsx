let priorityQueueBy<'a, 'b when 'b : comparison> =
  fun (pred : 'a -> 'b) ->
    (new System.Collections.Generic.List<('a * 'b)>())
    |> fun binaryHeap ->
      (
        (* enqueue *)
        (
          fun elem ->
            (
              [| binaryHeap.Count |],
              (binaryHeap.Add((elem, pred elem)))
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
                    |> fun (a, b) ->
                      if (a |> snd) > (b |> snd)
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
                      |> fun (a, b) ->
                        if (a |> snd) < (b |> snd)
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
          fun () -> binaryHeap |> Seq.map fst |> List.ofSeq
        )
      )

priorityQueueBy ((*) -1)
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
// [1; 2]
// enqueue 3
// [1; 2; 3]
// enqueue 4
// [1; 2; 3; 4]
// enqueue 5
// [1; 2; 3; 4; 5]
// enqueue 6
// [1; 2; 3; 4; 5; 6]
// enqueue 7
// [1; 2; 3; 4; 5; 6; 7]
// enqueue 8
// [1; 2; 3; 4; 5; 6; 7; 8]
// enqueue 9
// [1; 2; 3; 4; 5; 6; 7; 8; 9]
// dequeue 1
// [2; 4; 3; 8; 5; 6; 7; 9]
// dequeue 2
// [3; 4; 6; 8; 5; 9; 7]
// dequeue 3
// [4; 5; 6; 8; 7; 9]
// dequeue 4
// [5; 7; 6; 8; 9]
// dequeue 5
// [6; 7; 9; 8]
// dequeue 6
// [7; 8; 9]
// dequeue 7
// [8; 9]
// dequeue 8
// [9]
// dequeue 9
// []
