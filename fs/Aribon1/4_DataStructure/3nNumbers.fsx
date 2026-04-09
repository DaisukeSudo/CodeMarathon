// https://atcoder.jp/contests/arc074/tasks/arc074_b

// ***** library *****

let priorityQueueBy (prioritySelector: 'a -> 'b) (valueSelector: 'a -> 'c) =
  let heap = System.Collections.Generic.List<struct ('a * 'b)>()
  let mutable currentSum = LanguagePrimitives.GenericZero<'c>

  let inline swap i j =
    let tmp = heap.[i]
    heap.[i] <- heap.[j]
    heap.[j] <- tmp

  // enqueue
  let enqueue (elem: 'a) =
    let priority = prioritySelector elem
    currentSum <- currentSum + valueSelector elem
    heap.Add(struct (elem, priority))

    let mutable i = heap.Count - 1
    while i > 0 do
      let j = (i - 1) / 2
      let struct (_, ki) = heap.[i]
      let struct (_, kj) = heap.[j]
      if ki < kj then
        swap i j
        i <- j
      else
        i <- 0

  // dequeue
  let dequeue () =
    if heap.Count = 0 then failwith "Queue is empty"

    let last = heap.Count - 1
    let struct (head, _) = heap.[0]

    currentSum <- currentSum - valueSelector head

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

  // sum
  let sum () = currentSum

  (enqueue, dequeue, sum)

// ***** main *****

let n   = stdin.ReadLine() |> int
let a3n = stdin.ReadLine().Split() |> Array.map int64

let enqueueL, dequeueL, sumL = priorityQueueBy id id
let enqueueR, dequeueR, sumR = priorityQueueBy ((*) -1L) id

seq { 0 .. n - 1 }
|> Seq.iter (fun k -> enqueueL a3n.[k])
seq { n * 2 .. n * 3 - 1 }
|> Seq.iter (fun k -> enqueueR a3n.[k])

let sl = Array.zeroCreate (n + 1) in sl.[0] <- sumL() 
let sr = Array.zeroCreate (n + 1) in sr.[n] <- sumR() 

for i = n to n * 2 - 1 do
  enqueueL a3n.[i]
  dequeueL() |> ignore
  sl.[i - n + 1] <- sumL()

for i = n * 2 - 1 downto n do
  enqueueR a3n.[i]
  dequeueR() |> ignore
  sr.[i - n] <- sumR()

Array.map2 (-) sl sr
|> Array.max
|> stdout.WriteLine

// https://atcoder.jp/contests/arc074/submissions/74781672
