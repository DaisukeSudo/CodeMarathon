open System

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

let dijkstra n (adjacents: Map<int, int>[]) s g =
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
        adjacents.[v]
        |> Map.iter (fun v2 cost ->
          let newDist = d + cost
          if dist.[v2] > newDist then
            dist.[v2] <- newDist
            enqueue v2 newDist
        )

  if dist.[g] = System.Int32.MaxValue then -1 else dist.[g]

// ---- Test ----

// 0 --(2)-- 1 --(2)-- 4 --(1)-- 5
//  \       /          |         |
//  (3)   (4)         (6)       (3)
//    \   /            |         |
//      2 --(1)-- 3 --(5)------- 6

let n = 7
let es = [
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

let adjacents = Array.create (n + 1) Map.empty in
  es |> List.iter (fun (u, v, c) ->
    adjacents.[u] <- adjacents.[u] |> Map.add v c
    adjacents.[v] <- adjacents.[v] |> Map.add u c
  )

printfn "=== 全頂点対の最短距離照会 ==="
[0 .. n - 1] |> List.iter (fun s ->
  [0 .. n - 1]
  |> List.map (fun g -> dijkstra (n - 1) adjacents s g)
  |> List.toArray
  |> printfn "%d: %A" s
)

// 0: [|0; 2; 3; 4; 4; 5; 8|]
// 1: [|2; 0; 5; 6; 2; 3; 6|]
// 2: [|3; 5; 0; 1; 4; 5; 6|]
// 3: [|4; 6; 1; 0; 5; 6; 5|]
// 4: [|4; 2; 4; 5; 0; 1; 4|]
// 5: [|5; 3; 5; 6; 1; 0; 3|]
// 6: [|8; 6; 6; 5; 4; 3; 0|]

printfn "\n=== 特殊ケースの検証 ==="

printfn "孤立した頂点への経路照会 (-1 になるか)"
printfn "頂点 7 を追加（どこにも接続されていない）"
let isolatedNode = 7
let unreachableCost = dijkstra isolatedNode adjacents 0 isolatedNode
printfn "非連結頂点 (0 -> 7): %d (期待値: -1)" unreachableCost
