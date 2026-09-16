// https://atcoder.jp/contests/abc035/tasks/abc035_d

// ---- Lib ----

let inline __builtin_clz (x: int64) =
  let rec loop n = 
    if (x >>> n) &&& 1L = 1L then n else loop (n - 1)
  loop 63

let inline priorityQueue () =
  let heap = Array.create 65 []
  let mutable last = 0L
  let mutable size = 0

  let inline index (x: int64) =
    if x = last then 0 else __builtin_clz (x ^^^ last)

  let inline enqueue v (x: int64) =
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

  let inline isEmpty () = size = 0

  (enqueue, dequeue, isEmpty)

let dijkstra n (es: (int * int64) list[]) s =
  let dist = Array.create (n + 1) 1_000_000_000_000_000_000L
  let enqueue, dequeue, isEmpty = priorityQueue ()

  dist.[s] <- 0L
  enqueue s 0L

  while not (isEmpty ()) do
    let (v, d) = dequeue ()
    if d <= dist.[v] then
      for (v2, cost) in es.[v] do
        let newDist = d + cost
        if dist.[v2] > newDist then
          dist.[v2] <- newDist
          enqueue v2 newDist
  dist

// ---- Main ----

let n, m, t = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int64 x.[2]
let a = stdin.ReadLine().Split() |> Array.map int64 |> Array.append [| 0L |] // 1-indexed用
let abc = Array.init m (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int64 x.[2])

let go = Array.create (n + 1) [] // Outbound Graph
let gi = Array.create (n + 1) [] // Inbound Graph

for u, v, c in abc do
  go.[u] <- (v, c) :: go.[u]
  gi.[v] <- (u, c) :: gi.[v]

let distTo   = dijkstra n go 1  // 町１から各町への往路最短時間
let distFrom = dijkstra n gi 1  // 各町から町１への復路最短時間

let mutable ans = 0L

for i in 1 .. n do
  let travelTime = distTo.[i] + distFrom.[i]
  if travelTime < t then
    let stayTime = t - travelTime
    let reward = stayTime * a.[i]
    ans <- max ans reward

ans |> stdout.WriteLine

// https://atcoder.jp/contests/abc035/submissions/79296763
