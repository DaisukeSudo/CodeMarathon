// https://atcoder.jp/contests/typical90/tasks/typical90_i

let a = 180. / System.Math.PI

let n = stdin.ReadLine() |> int
let xs = Array.init n (fun _ ->
  stdin.ReadLine().Split()
  |> Array.map float
  |> fun x -> new System.Numerics.Complex(x.[0], x.[1])
)

xs
|> Array.map (fun xi ->
  let ys =
    xs
    |> Array.filter ((<>) xi)
    |> Array.map (fun xj -> (xj - xi).Phase * a)
    |> Array.sort

  let mutable i = 1
  let mutable j = 0
  let mutable ans = 0.

  while i < n - 1 do
    let d = ys.[i] - ys.[j]
    if d <= 180.
    then i <- i + 1
    else j <- j + 1
    ans <- max ans (180. - (abs (180. - d)))
  ans
)
|> Array.max
|> printfn "%A"

// https://atcoder.jp/contests/typical90/submissions/32146518
