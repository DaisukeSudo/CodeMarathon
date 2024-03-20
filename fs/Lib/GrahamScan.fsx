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

[|
  (0, 0)
  (1, 1)
  (2, 2)
  (3, 3)
|]
|> Array.map (Vec2.fromTuple)
|> grahamScan
|> Array.map (Vec2.toTuple)
|> printfn "%A"
