// https://atcoder.jp/contests/typical90/tasks/typical90_aq

let guard t arr = Array.concat [ [| t |]; arr; [| t |] ]
let guard2 t h w ilzr = Array.init h (ilzr >> guard t) |> guard (Array.create (w + 2) t)
let createArray2 i j v = Array.init i (fun _ -> Array.create j v)

// ----

let aisle = '.'
let unexplored = 1_000_000

let h, w   = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let rs, cs = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let rt, ct = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let field  = guard2 false h w (ignore >> stdin.ReadLine >> Seq.map ((=) aisle) >> Seq.toArray)

let dp = createArray2 (h + 1) (w + 1) unexplored
dp.[rs].[cs] <- -1

let queue = System.Collections.Generic.Queue<int * int>()
queue.Enqueue (rs, cs)

while queue.Count > 0 do
  let y1, x1 = queue.Dequeue ()   // current position

  [(1, 0); (-1, 0); (0, -1); (0, 1)]
  |> List.iter (fun (dy, dx) ->   // direction
    let mutable y2 = y1 + dy
    let mutable x2 = x1 + dx
    let mutable next = true

    while next && field.[y2].[x2] do
      match (dp.[y1].[x1] + 1, dp.[y2].[x2]) with
      | (v1, v2) when v1 > v2 ->  // already arrived at a lower cost (it also prevents reverse stepping)
        next <- false
      | (v1, v2) when v1 < v2 ->  // stepped at a lower cost than ever before
        dp.[y2].[x2] <- v1
        queue.Enqueue (y2, x2)
      | _ -> ()                   // (might be orthogonal to other paths)

      // keep going straight
      y2 <- y2 + dy
      x2 <- x2 + dx
  )

dp.[rt].[ct]
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/38907113
