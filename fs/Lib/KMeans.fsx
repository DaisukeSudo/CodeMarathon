let rec alternate l1 l2 =
  match l1, l2 with
  | [], [] -> []
  | [], _ -> l2
  | _, [] -> l1
  | x :: xs, y :: ys -> x :: y :: (alternate xs ys)

let rec pairs l =
  match l with
  | [] -> []
  | x :: xs -> List.map (fun y -> (x, y)) xs @ pairs xs


type Point = { x : float; y : float }

module Point =

  let distance p1 p2 = 
    let dx = p1.x - p2.x
    let dy = p1.y - p2.y
    dx * dx + dy * dy

  let centroid ps = 
    let len  = ps |> Seq.length |> float
    let sumX = ps |> Seq.sumBy (fun p -> p.x)
    let sumY = ps |> Seq.sumBy (fun p -> p.y)
    { x = sumX / len; y = sumY / len }

  let maxDistance ps =
    ps |> pairs |> List.map (fun (p1, p2) -> distance p1 p2) |> List.fold (max) 0.


type Cluster = { centroid : Point; points : Point list }

module Cluster =

  let initialize ps k =
    let ps1 = ps |> Seq.sortBy (Point.distance { x = 0.; y = 0. }) |> Seq.toList
    let ps2 = ps |> List.rev
    alternate ps1 ps2
    |> List.take k
    |> List.map (fun p -> { centroid = p; points = [] })
    |> List.toArray

  let assign cs p =
    let ds = cs |> Array.map (fun c -> Point.distance p c.centroid)
    let m  = ds |> Array.min
    let i  = ds |> Array.findIndex ((=) m)
    cs.[i] <- { cs.[i] with points = p :: cs.[i].points }

  let resetPoints (cs: Cluster []) =
    [0 .. cs.Length - 1] |> Seq.iter (fun i ->
      cs.[i] <- { cs.[i] with points = [] }
    )

  let reassign ps cs =
    cs |> resetPoints
    ps |> Seq.iter (assign cs)

  let updateCentroid (cs: Cluster []) =
    (false, [0 .. cs.Length - 1]) ||> Seq.fold (fun acc i ->
      let newCentroid = cs.[i].points |> Point.centroid
      let updated = cs.[i].centroid <> newCentroid
      if updated then cs.[i] <- { cs.[i] with centroid = newCentroid }
      acc || updated
    )

let kMeans ps k =
  let cs = Cluster.initialize ps k
  let mutable converged = false
  while not converged do
    cs |> Cluster.reassign ps
    converged <- cs |> Cluster.updateCentroid |> not
  cs

// ----

let k = 3
let ps = [
  { x = 0.; y = 0. }
  { x = 1.; y = 1. }
  { x = 0.; y = 2. }
  { x = 2.; y = 3. }
  { x = 3.; y = 1. }
]

let cs = kMeans ps k
cs |> printfn "clusters: %A"
cs |> Array.map (fun c -> Point.maxDistance c.points) |> printfn "max distance: %A"
