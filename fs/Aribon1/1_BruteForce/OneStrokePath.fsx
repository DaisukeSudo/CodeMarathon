// https://atcoder.jp/contests/abc054/tasks/abc054_c

open System.Collections.Generic

let n, m = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let ab = Array.init m (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

let adjacent = Array.create n List.empty
ab |> Seq.iter (fun (a, b) ->
  adjacent.[a] <- b :: adjacent.[a]
  adjacent.[b] <- a :: adjacent.[b]
)

let goal = (1 <<< n) - 1

let queue = Queue<int * int>() in queue.Enqueue(0, 0)
let mutable ans = 0

while queue.Count > 0 do
  let node, visited = queue.Dequeue()
  let visited2 = visited ||| (1 <<< node)

  if visited2 = goal then
    ans <- ans + 1
  else
    adjacent.[node]
    |> List.filter (fun node2 -> (visited2 >>> node2) &&& 1 = 0)
    |> List.iter (fun node2 -> queue.Enqueue((node2, visited2)))

ans |> stdout.WriteLine

// https://atcoder.jp/contests/abc054/submissions/52740872
