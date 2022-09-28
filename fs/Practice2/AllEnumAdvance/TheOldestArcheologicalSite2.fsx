// https://atcoder.jp/contests/joi2007ho/tasks/joi2007ho_c

let n = stdin.ReadLine() |> int
let cs = [| for _ in 1 .. n -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1] |] |> Array.sort

let createArray2 iSize jSize value = Array.init iSize (fun _ -> Array.create jSize value)
let field = createArray2 5001 5001 false in cs |> Array.iter (fun (x, y) -> field.[x].[y] <- true)

let insideOfField (x, y) =
  [x; y] |> Seq.forall (fun x -> 0 <= x && x <= 5000) && field.[x].[y]

let squareArea (x1, y1) (x2, y2) =
  let dx = x2 - x1
  let dy = y2 - y1
  if insideOfField (x1 - dy, y1 + dx) && insideOfField (x2 - dy, y2 + dx)
  then dx * dx + dy * dy else 0

seq {
  for i in     0 .. n - 2 do
  for j in i + 1 .. n - 1 ->
  squareArea cs.[i] cs.[j]
}
|> Seq.max
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2007ho/submissions/35216734
