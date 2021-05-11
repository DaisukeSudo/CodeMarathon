// https://atcoder.jp/contests/joi2016yo/tasks/joi2016yo_e

// let stdin = new System.IO.StreamReader("2016-yo-t5-in5.txt")

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
  (*
    n: Number of cities
    m: Number of roads
    k: Number of cities with zombies
    s: Distance zombies can reach
    p: Fee in a safe city
    q: Fee in a dangerous city
  *)
  (
    (stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1], x.[2], x.[3])),
    (stdin.ReadLine().Split() |> Array.map int64 |> fun x -> (x.[0], x.[1]))
  )
  |> fun ((n, m, k, s), (p, q)) ->
    (
      Array.init k (fun _ -> stdin.ReadLine() |> int),
      Array.init m (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])),
      Array.create (n + 1) [],
      Array.create (n + 1) false,
      Array.create (n + 1) false,
      Array.create (n + 1) System.Int64.MaxValue,
      Array.create (n + 1) false
    )
    |> fun (cs, rs, routes, zombies, dangers, costs, passed) ->
      (
        (* set routes *)
        rs
        |> Array.iter (fun (a, b) ->
          (
            (routes.[a] <- b::routes.[a]),
            (routes.[b] <- a::routes.[b])
          )
          |> ignore
        ),
        (* set zombies *)
        cs |> Array.iter (fun c -> zombies.[c] <- true),
        (* set dangers *)
        (
          (
            [| cs |> Array.map (fun c -> (c, 0)) |> Seq.toList |],
            Array.create (n + 1) System.Int32.MaxValue
          )
          |> fun (stack, evaluated) ->
            Seq.initInfinite ignore
            |> Seq.takeWhile (fun _ -> stack.[0] |> List.isEmpty |> not)
            |> Seq.map (fun _ -> stack.[0] |> List.head |> fun h -> (stack.[0] <- stack.[0] |> List.tail) |> fun _ -> h)
            |> Seq.iter (fun (x, d) ->
              (dangers.[x] <- true)
              |> fun _ ->
                if d < s
                then
                  routes.[x]
                  |> List.filter (fun nx -> d + 1 < evaluated.[nx])
                  |> List.iter (fun nx -> stack.[0] <- (nx, d + 1) :: stack.[0])
              |> fun _ -> evaluated.[x] <- d
            )
        ),
        (* priority queue *)
        (
          priorityQueueBy (fun x -> costs.[x] * -1L)
        )
      )
      |> fun (_, _, _, (enqueue, dequeue, isEmpty)) ->
        (* the move starts from here *)
        (
          (costs.[1] <- 0L),
          enqueue 1
        )
        |> fun _ ->
          Seq.initInfinite ignore
          |> Seq.takeWhile (fun _ -> not (isEmpty () || passed.[n]))
          |> Seq.map dequeue
          |> Seq.filter (fun x -> not (passed.[x] || zombies.[x]))
          |> Seq.iter (fun x1 ->
            routes.[x1]
            |> List.iter (
              fun x2 ->
                (if dangers.[x2] then q else p)
                |> fun cost ->
                  if costs.[x2] > costs.[x1] + cost
                  then (costs.[x2] <- costs.[x1] + cost) |> fun _ -> enqueue x2
            )
            |> fun _ -> passed.[x1] <- true
          )
          |> fun _ -> costs.[n] - (if dangers.[n] then q else p)
  |> printfn "%d"

// https://atcoder.jp/contests/joi2016yo/submissions/22468742
