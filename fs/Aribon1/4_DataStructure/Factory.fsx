// https://atcoder.jp/contests/code-thanks-festival-2017-open/tasks/code_thanks_festival_2017_c

// ***** library *****

let priorityQueueBy<'a, 'b when 'b : comparison> (pred : 'a -> 'b) =
  let heap = System.Collections.Generic.List<struct ('a * 'b)>()
 
  let inline swap i j =
    let tmp = heap.[i]
    heap.[i] <- heap.[j]
    heap.[j] <- tmp
 
  // enqueue
  let enqueue (elem: 'a) =
    heap.Add(struct (elem, pred elem))
    let mutable i = heap.Count - 1
    while i > 0 do
      let j = (i - 1) / 2
      let struct (_, ki) = heap.[i]
      let struct (_, kj) = heap.[j]
      if ki < kj then
        swap i j
        i <- j
      else
        i <- 0 // break
 
  // dequeue
  let dequeue () =
    let last = heap.Count - 1
    let struct (head, _) = heap.[0]
    heap.[0] <- heap.[last]
    heap.RemoveAt(last)
    if heap.Count > 0 then
      let mutable i = 0
      let mutable cont = true
      while cont do
        let j = i * 2 + 1
        if j >= heap.Count then
          cont <- false
        else
          let k =
            if j + 1 >= heap.Count then j
            else
              let struct (_, kj)  = heap.[j]
              let struct (_, kj1) = heap.[j + 1]
              if kj < kj1 then j else j + 1
          let struct (_, ki) = heap.[i]
          let struct (_, kk) = heap.[k]
          if ki > kk then
            swap i k
            i <- k
          else
            cont <- false
    head
 
  (enqueue, dequeue)

// ***** main *****

let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int64 x.[1]
let ab   = Array.init n (fun _ -> stdin.ReadLine().Split() |> fun x -> int64 x.[0], int64 x.[1])

let enqueue, dequeue = priorityQueueBy fst

ab |> Array.iter (fun (a, b) -> enqueue (a, b))

seq { 1L .. k } |> Seq.fold (fun acc _ ->
  let (c, b) = dequeue ()
  enqueue (c + b, b)
  acc + c
) 0L
|> stdout.WriteLine

// https://atcoder.jp/contests/code-thanks-festival-2017-open/submissions/74258795
