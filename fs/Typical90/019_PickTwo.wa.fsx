// https://atcoder.jp/contests/typical90/tasks/typical90_s

let inline __builtin_clz x =
  let rec loop n = if x >>> n &&& 1 = 1 then n else loop (n - 1)
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
      heap.[ai] |> List.iter (fun (_, x as p) -> index x |> fun i -> heap.[i] <- p :: heap.[i])
      heap.[ai] <- []
    match heap.[0] with
    | h :: ts ->
      heap.[0] <- ts
      h
    | _ -> failwith "heap is empty"

  let inline isEmpty () =
    size = 0

  (enqueue, dequeue, isEmpty)

// ----

type Chain =
  val value: int
  val mutable prev: Chain option
  val mutable next: Chain option
  val mutable isAlive: bool
  new (v, p, n) = {
    value = v
    prev = p
    next = n
    isAlive = true
  }
  with override this.ToString() = sprintf "(%A, %A)" this.value this.isAlive

module Chain =
  let cost (a: Chain) =
    a.prev
    |> Option.map (fun x -> x.value - a.value |> abs)
    |> Option.defaultValue System.Int32.MaxValue

  let init v =
    new Chain(v, None, None)

  let join p v =
    let a = new Chain(v, Some p, None)
    p.next <- Some a
    a

  let drop (a: Chain) =
    a.isAlive <- false
    a.prev |> Option.iter (fun p -> p.isAlive <- false)
    let n = a.next
    let p = a.prev |> Option.bind (fun p -> p.prev)
    n |> Option.iter (fun n -> n.prev <- p)
    p |> Option.iter (fun p -> p.next <- n)
    n

// ----

let solve vs =
  let enqueue, dequeue, isEmpty = priorityQueue ()

  (vs |> Seq.head |> Chain.init, vs |> Seq.tail) ||> Seq.fold (fun p v ->
    let a = Chain.join p v in enqueue a (Chain.cost a); a
  ) |> ignore

  Seq.initInfinite ignore
  |> Seq.takeWhile (isEmpty >> not)
  |> Seq.map dequeue
  |> Seq.filter (fun (a, c) -> a.isAlive && c = Chain.cost a)
  |> Seq.fold (fun acc (a, c) ->
    Chain.drop a |> Option.iter (fun a -> enqueue a (Chain.cost a))
    acc + c
  ) 0

stdin.ReadLine() |> ignore
stdin.ReadLine().Split() |> Array.map int |> solve |> stdout.WriteLine
