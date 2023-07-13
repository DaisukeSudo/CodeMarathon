// https://atcoder.jp/contests/typical90/tasks/typical90_bi

let deck q =
  let deckL = Array.create q -1
  let deckR = Array.create q -1
  let mutable indexL = 0
  let mutable indexR = 0

  let appendL x = deckL.[indexL] <- x; indexL <- indexL + 1
  let appendR x = deckR.[indexR] <- x; indexR <- indexR + 1

  let get i =
    if i < indexL
    then deckL.[indexL - i - 1]
    else deckR.[i - indexL]

  (appendL, appendR, get)

// ----

let q = stdin.ReadLine() |> int
let tx = Array.init q (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])

let (appendL, appendR, get) = deck q

let mutable ans = []
tx |> Array.iter (fun (t, x) ->
  match t with
  | 1 -> appendL x
  | 2 -> appendR x
  | _ -> ans <- (get (x - 1)) :: ans
)

ans |> Seq.rev |> Seq.iter stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/43523794
