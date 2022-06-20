// https://atcoder.jp/contests/typical90/tasks/typical90_l

let unionFindTree =
  fun n ->
    let tbl = [| 0..n - 1 |]

    let findTree = 
      fun x ->
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

    let updateRoot =
      fun newRoot -> findTree >> (fun tree ->
        tree |> List.iter (fun x -> tbl.[x] <- newRoot)
      )

    let unite = fun a b -> updateRoot (findRoot a) b

    let same = fun a b -> (findRoot a) = (findRoot b)

    (unite, same)

// ----

let unionFindTree2 =
  fun h w ->
    let unite, same = unionFindTree ((h + 2) * (w + 2))

    let white = false
    let red = true
    let key = fun r c -> r * w + c

    let colors = Array.init (h + 2) (fun _-> Array.create (w + 2) white)

    let paint = fun r c -> colors.[r].[c] <- red

    let unite2 =
      fun r1 c1 r2 c2 ->
        if colors.[r2].[c2] then
          unite (key r1 c1) (key r2 c2)

    let same2 =
      fun r1 c1 r2 c2 ->
        colors.[r1].[c1] && colors.[r2].[c2] && same (key r1 c1) (key r2 c2)

    (paint, unite2, same2)

// ----

let h, w = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]

let paint, unite2, same2 = unionFindTree2 h w

let process1 =
  fun r1 c1 ->
    paint r1 c1
    [(r1 - 1, c1); (r1 + 1, c1); (r1, c1 - 1); (r1, c1 + 1)]
    |> List.iter (fun (r2, c2) -> unite2 r1 c1 r2 c2)

let process2 =
  fun r1 c1 r2 c2 ->
    if same2 r1 c1 r2 c2 then "Yes" else "No"
    |> stdout.WriteLine

type Query = 
  | Query1 of int * int
  | Query2 of int * int * int * int

module Query =
  let create (v: int []) =
    if v.Length = 3
    then Query1 (v.[1], v.[2])
    else Query2 (v.[1], v.[2], v.[3], v.[4])

  let handle (q: Query) =
    match q with
    | Query1(r, c)            -> process1 r c
    | Query2(r1, c1, r2, c2)  -> process2 r1 c1 r2 c2

let q = stdin.ReadLine() |> int
[1..q] |> List.iter (fun _ ->
  stdin.ReadLine().Split()
  |> Array.map int
  |> Query.create
  |> Query.handle
)

// https://atcoder.jp/contests/typical90/submissions/32617582
