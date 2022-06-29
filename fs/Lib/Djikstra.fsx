let inline __builtin_clz x =
  let rec loop n = 
    if x >>> n &&& 1 = 1
    then n
    else loop (n - 1)
  loop 31

let inline priorityQueue () =
  let heap = Array.create 33 []
  let mutable last = 0
  let mutable size = 0

  let inline index x =
    if x = last then 0 else __builtin_clz (x ^^^ last)

  let inline enqueue v x =
    size <- size + 1
    let p = (v, x)
    let i = index x
    heap.[i] <- p :: heap.[i]

  let inline dequeue () =
    size <- size - 1
    if heap.[0] |> List.length |> (=) 0 then
      let ai = heap |> Array.findIndex (List.length >> ((<>) 0))
      last <- heap.[ai] |> List.minBy snd |> snd
      heap.[ai] |> List.iter (fun (_, x as p) ->
        let i = index x
        heap.[i] <- p :: heap.[i]
      )
      heap.[ai] <- []
    match heap.[0] with
    | h :: ts ->
      heap.[0] <- ts
      h
    | _ -> failwith "heap is empty"

  let inline isEmpty () =
    size = 0

  (enqueue, dequeue, isEmpty)

let inline djikstra n es s =
  let adjacents = Array.create n Map.empty
  es |> Seq.iter (fun (s, e, d) ->
    adjacents.[s] <- adjacents.[s] |> Map.add e d
    adjacents.[e] <- adjacents.[e] |> Map.add s d
  )

  let dist = Array.create n System.Int32.MaxValue
  let fin = Array.create n false
  let enqueue, dequeue, isEmpty = priorityQueue ()

  dist.[s] <- 0
  enqueue s 0
  Seq.initInfinite ignore
  |> Seq.takeWhile (isEmpty >> not)
  |> Seq.map dequeue
  |> Seq.filter (fun (v, _) -> not fin.[v])
  |> Seq.iter (fun (v, _) ->
    adjacents.[v]
    |> Map.filter (fun v2 cost ->
      dist.[v2] > dist.[v] + cost
    )
    |> Map.iter (fun v2 cost ->
      dist.[v2] <- dist.[v] + cost
      enqueue v2 dist.[v2]
    )
    fin.[v] <- true
  )
  dist

// ----

let n = 7
let es =
  [
    (0, 1, 2)
    (0, 2, 3)
    (1, 4, 2)
    (2, 3, 1)
    (2, 4, 4)
    (3, 6, 5)
    (4, 5, 1)
    (4, 6, 6)
    (5, 6, 3)
  ]

let fn = djikstra n es
[0..n - 1] |> List.iter (fun s -> printfn "%d: %A" s (fn s))

// 0: [|0L; 2L; 3L; 4L; 4L; 5L; 8L|]
// 1: [|2L; 0L; 5L; 6L; 2L; 3L; 6L|]
// 2: [|3L; 5L; 0L; 1L; 4L; 5L; 6L|]
// 3: [|4L; 6L; 1L; 0L; 5L; 6L; 5L|]
// 4: [|4L; 2L; 4L; 5L; 0L; 1L; 4L|]
// 5: [|5L; 3L; 5L; 6L; 1L; 0L; 3L|]
// 6: [|8L; 6L; 6L; 5L; 4L; 3L; 0L|]
