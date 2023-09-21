// https://atcoder.jp/contests/typical90/tasks/typical90_br

let n = stdin.ReadLine() |> int
let x, y = Array.init n (fun _ -> stdin.ReadLine().Split() |> fun x -> float x.[0], float x.[1]) |> Array.unzip

let median arr =
  let a = arr |> Array.sort
  let l = arr |> Array.length
  if l % 2 = 0
  then (a.[l / 2 - 1] + a.[l / 2]) / 2.
  else a.[l / 2]

let inconvenience arr =
  let m = median arr
  arr |> Array.sumBy (((-) m) >> abs)

[x; y] |> List.sumBy inconvenience |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/45766609
