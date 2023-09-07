// https://atcoder.jp/contests/typical90/tasks/typical90_bp

let unionFindTree n =
  let mutable cnt = n
  let tbl = [| 0..n - 1 |]

  let findTree x =
    let mutable next = true
    Seq.initInfinite ignore
    |> Seq.takeWhile (fun () -> next)
    |> Seq.scan (fun c _ ->
      let parent = tbl.[c]
      if parent = c then next <- false
      parent
    ) x
    |> Seq.rev
    |> Seq.toList

  let findRoot =
    findTree >> (fun tree ->
      let root = tree |> List.head
      tree |> List.tail |> List.iter (fun x -> tbl.[x] <- root)
      root
    )

  let updateRoot newRoot =
    findTree >> (fun tree ->
      let oldRoot = tree |> Seq.head
      tree |> List.iter (fun x -> tbl.[x] <- newRoot)
      oldRoot
    )

  let unite a b =
    let rA = findRoot a
    let rB = updateRoot rA b
    if (rA <> rB) then cnt <- cnt - 1

  let same a b = (findRoot a) = (findRoot b)

  (unite, same)

// ----

let n = stdin.ReadLine() |> int
let q = stdin.ReadLine() |> int
let txyv = Array.init q (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1] - 1, x.[2] - 1, int64 x.[3])

let (unite, same) = unionFindTree n

let d = Array.create (n + 1) 0L
let s = Array.create (n + 1) 0L

for t, x, _, v in txyv do
  if t = 0 then s.[x] <- v

for i in 0 .. n - 1 do
  d.[i + 1] <- s.[i] - d.[i]

seq {
  for t, x, y, v in txyv do
    if t = 0 then
      unite x y
    else
      if not (same x y) then
        yield None
      else
        if (x + y) % 2 = 0 then
          yield Some(v + d.[y] - d.[x])
        else
          yield Some(d.[x] + d.[y] - v)
}
|> Seq.map (fun x -> match x with | Some(x) -> sprintf "%d" x | None -> "Ambiguous")
|> Seq.iter (stdout.WriteLine)

// https://atcoder.jp/contests/typical90/submissions/45299030
