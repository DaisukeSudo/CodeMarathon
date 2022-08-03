// https://atcoder.jp/contests/typical90/tasks/typical90_r

let PI = System.Math.PI

let pos t l e =
  let d = (360. * (1. - e / t) - 90.) % 360. * PI / 180.
  let r = l / 2.
  let x = 0.
  let y = r * cos d
  let z = r * (1. + sin d)
  (x, y, z)

let ang x y z =
  let b = sqrt (x * x + y * y)
  (atan2 z b) * 180. / PI

let solve t l x y e =
  let _, y1, z1 = pos t l e
  ang x (y - y1) z1

// ----

let t = stdin.ReadLine() |> float
let l, x, y = stdin.ReadLine().Split() |> Array.map float |> fun x -> x.[0], x.[1], x.[2]
let q = stdin.ReadLine() |> int

[1 .. q] |> List.iter (
  ignore
  >> stdin.ReadLine
  >> float
  >> solve t l x y
  >> stdout.WriteLine
)

// https://atcoder.jp/contests/typical90/submissions/33741468
