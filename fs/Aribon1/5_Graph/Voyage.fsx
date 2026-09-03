// https://atcoder.jp/contests/joi2008yo/tasks/joi2008yo_f

// ---- Lib ----

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

let dijkstra n (es: Map<int, int>[]) s g =
  let dist = Array.create (n + 1) System.Int32.MaxValue
  let enqueue, dequeue, isEmpty = priorityQueue ()

  dist.[s] <- 0
  enqueue s 0

  let mutable found = false
  while not (isEmpty ()) && not found do
    let (v, d) = dequeue ()
    
    if d <= dist.[v] then
      if v = g then
        found <- true
      else
        es.[v]
        |> Map.iter (fun v2 cost ->
          let newDist = d + cost
          if dist.[v2] > newDist then
            dist.[v2] <- newDist
            enqueue v2 newDist
        )

  if dist.[g] = System.Int32.MaxValue then -1 else dist.[g]

// ---- Main ----

let n, k  = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let qs    = Array.init k (fun _ -> stdin.ReadLine().Split() |> Array.map int)

let es = Array.create (n + 1) Map.empty

let query a b = dijkstra (n + 1) es a b

let updateNode src dst cost =
    let newCost =
      match es.[src] |> Map.tryFind dst with
      | Some current -> min current cost
      | None -> cost
    es.[src] <- es.[src] |> Map.add dst newCost

([], qs) ||> Array.fold (fun ans q ->
  match q with
  // order
  | [| 0; a; b |] ->
    (query a b) :: ans
  // commence
  | [| 1; c; d; e |] ->
    updateNode c d e
    updateNode d c e
    ans
  | _ -> failwith "ng"
)
|> Seq.rev
|> Seq.iter stdout.WriteLine
