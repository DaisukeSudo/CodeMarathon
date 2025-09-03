// https://atcoder.jp/contests/atc002/tasks/atc002_c

// ***** library *****

let priorityQueueBy<'a, 'b when 'b : comparison> (pred : 'a -> 'b) =
  let binaryHeap = System.Collections.Generic.List<('a * 'b)>()
  (
    // enqueue
    (fun (elem : 'a) ->
      let mutable x = binaryHeap.Count
      binaryHeap.Add(elem, pred elem)
      
      while x > 0 do
        let i = x
        let j = (i - 1) / 2
        let (a, b) = (binaryHeap.[i], binaryHeap.[j])
        if snd a < snd b then
          binaryHeap.[i] <- b
          binaryHeap.[j] <- a
          x <- j
        else
          x <- 0
    ),
    // dequeue
    (fun () ->
      let n = binaryHeap.Count - 1
      let head = fst binaryHeap.[0]
      binaryHeap.[0] <- binaryHeap.[n]
      binaryHeap.RemoveAt(n)

      if binaryHeap.Count > 0 then
        let mutable x = 0
        let mutable isLooping = true
        while isLooping do
          let i = x
          let j = i * 2 + 1
          if j >= binaryHeap.Count then
            isLooping <- false
          else
            let k =
              if j = binaryHeap.Count - 1 || snd binaryHeap.[j] < snd binaryHeap.[j + 1]
              then j 
              else j + 1
            
            let (a, b) = (binaryHeap.[i], binaryHeap.[k])
            if snd a > snd b then
              binaryHeap.[i] <- b
              binaryHeap.[k] <- a
              x <- k
            else
              isLooping <- false
      head
    )
  )

// ***** main *****

let solve (ws: int64 array): int64 =
  let n = ws.Length

  let ws2 = Array.zeroCreate<int64> (n + 2)
  Array.blit ws 0 ws2 1 n

  let l = Array.init (n + 2) (fun i -> i - 1)
  let r = Array.init (n + 2) (fun i -> i + 1)

  let enqueue, dequeue = priorityQueueBy fst

  let pairCosts = Array.zeroCreate<int64> (n + 2)

  for i in 1 .. n - 1 do
    let cost = ws2.[i] + ws2.[i + 1]
    pairCosts.[i] <- cost
    enqueue (cost, i)

  let mutable totalCost = 0L

  let isValid i originalCost =
    r.[i] > 0 && pairCosts.[i] = originalCost

  let rec findValidPair () =
    let (cost, i) = dequeue ()
    if isValid i cost
    then (i, cost)
    else findValidPair ()

  for _ in 1 .. n - 1 do
    let i, currentCost = findValidPair ()
    
    ws2.[i] <- currentCost
    totalCost <- totalCost + currentCost
    
    let j = r.[i]
    let prev = l.[i]
    let next = r.[j]
    
    r.[prev] <- i
    l.[i] <- prev
    r.[i] <- next
    l.[next] <- i
    
    r.[j] <- -1
    pairCosts.[j] <- -1L
    pairCosts.[prev] <- -1L
    
    if prev > 0 then
      let newPrevCost = currentCost + ws2.[prev]
      pairCosts.[prev] <- newPrevCost
      enqueue (newPrevCost, prev)
    
    if next <= n then
      let newNextCost = currentCost + ws2.[next]
      pairCosts.[i] <- newNextCost
      enqueue (newNextCost, i)

  totalCost

// ----

let _n = stdin.ReadLine()
let ws = stdin.ReadLine().Split() |> Array.map int64

ws |> solve |> stdout.WriteLine

// https://atcoder.jp/contests/atc002/submissions/68661258 TLE
