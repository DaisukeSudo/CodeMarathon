// https://atcoder.jp/contests/arc005/tasks/arc005_3

open System.Collections.Generic

let h, w = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let grid = Array.init h (fun _ -> stdin.ReadLine().ToCharArray())

let (sx, sy) = grid |> Seq.mapi (fun i row -> row |> Seq.mapi (fun j x -> if x = 's' then Some(i, j) else None) |> Seq.choose id) |> Seq.filter (Seq.isEmpty >> not) |> Seq.head |> Seq.head

let queue = Queue<int * int * int>() in queue.Enqueue(sx, sy, 0)
let visited = Array2D.create h w 9 in visited.[sx, sy] <- 0
let mutable goal = false

// bfs
while queue.Count > 0 && not goal do
  let x, y, p = queue.Dequeue()
  [(0, 1); (1, 0); (-1, 0); (0, -1)]
  |> List.map (fun (dx, dy) -> (x + dx, y + dy))
  |> List.iter (fun (x, y) ->
    if x >= 0 && x < h && y >= 0 && y < w && p < visited.[x, y] then
      match grid.[x].[y] with
      | 'g' -> goal <- true
      | '.' ->
        visited.[x, y] <- p
        queue.Enqueue((x, y, p))
      | _ ->
        if p < 2 then
          visited.[x, y] <- p + 1
          queue.Enqueue((x, y, p + 1))
  )

if goal then "YES" else "NO"
|> stdout.WriteLine

// https://atcoder.jp/contests/arc005/submissions/52222195
