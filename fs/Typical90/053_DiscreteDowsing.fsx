// https://atcoder.jp/contests/typical90/tasks/typical90_ba

let fib = [| 1; 2; 3; 5; 8; 13; 21; 34; 55; 89; 144; 233; 377; 610; 987; 1597 |]

let ask (mem: int[]) (pos: int) =
  if mem.[pos] = -1 then
    pos |> printfn "? %d"
    mem.[pos] <- stdin.ReadLine() |> int
  mem.[pos]

let solve _ =
  let n = stdin.ReadLine() |> int
  let mem = Array.create 1609 -1
  [0    ..n   ] |> List.iter(fun i-> mem.[i] <- -1)
  [n + 1..1600] |> List.iter(fun i-> mem.[i] <- -i)

  let mutable ans = 0
  if n <= 5 then
    ans <- max ans ([1 .. n] |> List.maxBy(ask mem))
  else
    let mutable cl = 0
    let mutable cr = 1597
    let mutable dl = 610
    let mutable dr = 987
    let mutable el = dl |> ask mem
    let mutable er = dr |> ask mem

    let fn () =
      ans <- [ans; el; er] |> List.max
      if el < er
      then cl <- dl; dl <- dr; dr <- -1; el <- er; er <- -1
      else cr <- dr; dr <- dl; dl <- -1; er <- el; el <- -1

    fn()
    for i in [12..(-1)..0] do
      if dl = -1 then
        dl <- cl + fib.[i]
        el <- dl |> ask mem
      elif dr = -1 then
        dr <- cr - fib.[i]
        er <- dr |> ask mem
      fn()

    ans <- max ans ([cl + 1..cr - 1] |> List.maxBy(ask mem))

  ans |> printfn "! %d"

// main
let t = stdin.ReadLine() |> int
[1 .. t] |> List.iter(solve)
