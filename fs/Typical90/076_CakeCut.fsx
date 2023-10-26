// https://atcoder.jp/contests/typical90/tasks/typical90_bx

let n = stdin.ReadLine() |> int
let a = stdin.ReadLine().Split() |> Array.map int64

(
  let overall = a |> Array.sum
  if overall % 10L <> 0L then false else
    let target = overall / 10L

    let a2 = Array.concat [a; a]
    let limit = n * 2 - 1
    let mutable si = 0
    let mutable ei = 0
    let mutable current = a2.[0]
    let mutable processing = true

    while current <> target && processing do
      if current < target then
        if ei < limit then
          ei <- ei + 1
          current <- current + a2.[ei]
        else
          processing <- false
      else if current > target then
        if si < limit then
          current <- current - a2.[si]
          si <- si + 1
        else
          processing <- false

    current = target
)
|> fun exists -> if exists then "Yes" else "No"
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/46943200
