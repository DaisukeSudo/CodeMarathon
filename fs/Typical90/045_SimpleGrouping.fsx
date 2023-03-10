// https://atcoder.jp/contests/typical90/tasks/typical90_as

let dinstance (x1, y1) (x2, y2) = 
  let dx = x1 - x2
  let dy = y1 - y2
  dx * dx + dy * dy

let updateAt i v list =
  let rec loop i list =
    match list with
    | [] -> []
    | _ :: ts when i = 0 -> v :: ts
    | h :: ts -> h :: (loop (i - 1) ts)
  loop i list

// ----

let n, k = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let ps   = [| 1 .. n |] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1])

let dist =
  let mutable m = Map.empty
  [0 .. ps.Length - 2] |> List.iter (fun i -> [i + 1 .. ps.Length - 1] |> List.iter (fun j ->
    let k = dinstance ps.[i] ps.[j]
    if m |> Map.containsKey k |> not then
      m <- m |> Map.add k [| List.empty |]
    m.[k].[0] <- (i, j) :: (j, i) :: m.[k].[0]
  ))
  m |> Map.toList |> List.sortBy (fun (k, _) -> k)

let exclusion x =
  let rec loop c =
    match c with
    | [] -> []
    | (k, _) :: _ when k > x -> c
    | _ :: ts -> loop ts
  loop dist |> List.collect (fun (_, v) -> v.[0]) |> Set.ofList

let canBeGroupedWith ex x g =
  ex
  |> Seq.filter (fun (e, _) -> e = x)
  |> Seq.forall (fun (_, e) -> g |> List.contains e |> not)

let isFulfilled x =
  let ex = exclusion x
  let rec loop i gs =
    if i = n then true
    else
      gs
      |> List.mapi (fun j g -> (j, g))
      |> List.filter (fun (_, g) -> g |> canBeGroupedWith ex i)
      |> List.map (fun (j, g) -> gs |> updateAt j (i :: g))
      |> List.exists (loop (i + 1))
      || (gs |> List.length < k) && loop (i + 1) ([i] :: gs)
  loop 0 []

let mutable l = -1L
let mutable r = 2_000_000_000_000_000_000L

while r - l > 1L do
  let m = l + (r - l) / 2L
  if m |> isFulfilled
  then r <- m
  else l <- m

r |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/39547342
