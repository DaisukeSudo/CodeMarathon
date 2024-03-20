// https://atcoder.jp/contests/arc004/tasks/arc004_1

open System.Collections.Generic

type Vec2 = { x: float; y: float }

module Vec2 =
  let fromTuple (x, y) =
    { x = float x; y = float y }

  let toTuple p =
    (p.x, p.y)

// cross product of vectors
let cross (o: Vec2) (a: Vec2) (b: Vec2) =
  (a.x - o.x) * (b.y - o.y) - (a.y - o.y) * (b.x - o.x)

// Square of the distance between two points
let distance (a: Vec2) (b: Vec2) =
  let dx = a.x - b.x
  let dy = a.y - b.y
  dx * dx + dy * dy

// Declination angle from base point
let declination (b: Vec2) (p: Vec2) =
  System.Math.Atan2(p.y - b.y, p.x - b.x)

// Find the convex hull using Graham's scanning method
let grahamScan (points: Vec2 array) =
  if points.Length < 3 then points else
    // Choose a base point
    let bp = Array.minBy (fun p -> (p.y, p.x)) points
    // Sort by declination and distance from base point
    let sorted = points |> Array.sortBy (fun p -> (declination bp p, distance bp p))

    let stack = ResizeArray<Vec2>()
    stack.Add sorted.[0]
    stack.Add sorted.[1]

    for x in sorted.[2..] do
      // Remove if the angle is more than 180 degrees (i.e. right turn)
      while stack.Count >= 2 && (cross stack.[stack.Count - 2] stack.[stack.Count - 1] x) <= 0. do
        stack.RemoveAt(stack.Count - 1)
      stack.Add x

    stack.ToArray()

// ----

let combinations (arr: 'a array) : ('a * 'a) array =
  [|
    for i in 0 .. arr.Length - 2 do
      for j in i + 1 .. arr.Length - 1 do
        yield (arr.[i], arr.[j])
  |]

// ----

let n = stdin.ReadLine() |> int
Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])
|> Array.map (Vec2.fromTuple)
|> grahamScan
|> combinations
|> Array.map (fun (p1, p2) -> distance p1 p2)
|> Array.max
|> sqrt
|> printfn "%f"

// https://atcoder.jp/contests/arc004/submissions/51471691
