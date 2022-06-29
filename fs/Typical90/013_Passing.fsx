// https://atcoder.jp/contests/typical90/tasks/typical90_m

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

let n, m = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let es = [1..m] |> List.map (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0] - 1, int x.[1] - 1, int x.[2])

let distS = djikstra n es 0
let distE = djikstra n es (n - 1)

[0..n - 1]
|> List.map (fun i -> distS.[i] + distE.[i])
|> List.iter stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/32839779
