// https://atcoder.jp/contests/joi2014yo/tasks/joi2014yo_e

// let stdin = new System.IO.StreamReader("2014-yo-t5-in5.txt")

(***** library *****)

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
        )
      )
)
|> fun priorityQueueBy ->

  (***** main *****)

  (stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1]))
  |> fun (n, k) ->
    (
      Array.init n (fun _ -> stdin.ReadLine().Split() |> fun x -> (int64 x.[0], int x.[1])),
      Array.init k (fun _ -> stdin.ReadLine().Split() |> fun x -> (int x.[0], int x.[1])),
      Array.create (n + 1) [],
      Array.create (n + 1) System.Int64.MaxValue,
      Array.create (n + 1) false
    )
    |> fun (ts, rs, routes, costs, passed) ->
      (
        (
          Array.concat [[|(0L, 0)|]; ts]
        ),
        rs
        |> Array.iter (fun (a, b) ->
          (
            (routes.[a] <- b::routes.[a]),
            (routes.[b] <- a::routes.[b])
          )
          |> ignore
        ),
        (
          priorityQueueBy (fun x -> costs.[x] * -1L)
        )
      )
      |> fun (taxis, _, (enqueue, dequeue, isEmpty)) ->
        (
          (costs.[1] <- 0L),
          enqueue 1
        )
        |> fun _ ->
          Seq.initInfinite ignore
          |> Seq.takeWhile (fun _ -> not (isEmpty () || passed.[n]))
          |> Seq.map dequeue
          |> Seq.filter (fun x -> not passed.[x])
          |> Seq.iter (fun x1 ->
            (
              [| routes.[x1] |> List.map (fun x2 -> (x2, 1)) |],
              (Array.create (n + 1) System.Int32.MaxValue |> fun x -> (x.[x1] <- 0) |> fun _ -> x),
              taxis.[x1]
            )
            |> fun (stack, evaluated, (cost, reach)) ->
              Seq.initInfinite ignore
              |> Seq.takeWhile (fun _ -> stack.[0] |> List.isEmpty |> not)
              |> Seq.map (fun _ -> stack.[0] |> List.head |> fun h -> (stack.[0] <- stack.[0] |> List.tail) |> fun _ -> h)
              |> Seq.iter (fun (x2, step) ->
                (
                  if costs.[x2] > costs.[x1] + cost
                  then (costs.[x2] <- costs.[x1] + cost) |> fun _ -> enqueue x2
                )
                |> fun _ -> evaluated.[x2] <- step
                |> fun _ ->
                  if step < reach
                  then
                    routes.[x2]
                    |> List.filter (fun x3 -> step + 1 < evaluated.[x3])
                    |> List.iter (fun x3 -> stack.[0] <- (x3, step + 1) :: stack.[0])
              )
            |> fun _ -> passed.[x1] <- true
          )
          |> fun _ -> costs.[n]
  |> printfn "%d"

// https://atcoder.jp/contests/joi2014yo/submissions/22541039
