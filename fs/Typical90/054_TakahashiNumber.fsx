// https://atcoder.jp/contests/typical90/tasks/typical90_bb

open System.Collections.Generic

// n: authors
// m: papers
let n, m = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]

// r: paper - co-authors
let r = Array.create m [| |]
for i = 0 to m - 1 do
  stdin.ReadLine() |> ignore
  r.[i] <- stdin.ReadLine().Split() |> Array.map (int >> ((+) -1))

// rp: author - paper
let rp = Array.create n []
for i = 0 to m - 1 do
  for j = 0 to r.[i].Length - 1 do
    let author = r.[i].[j]
    rp.[author] <- i :: rp.[author]

// bfs
let ans = Array.create n (-1) in ans.[0] <- 0
let queue = Queue<int * int>() in queue.Enqueue(0, 0)
let visited = Array.create m false

while queue.Count > 0 do
  let (author, dist) = queue.Dequeue()

  for paper in rp.[author] do
    if not visited.[paper] then
      visited.[paper] <- true

      for coauthor in r.[paper] do
        if ans.[coauthor] < 0 then
          ans.[coauthor] <- dist + 1
          queue.Enqueue(coauthor, dist + 1)

ans |> Array.iter (stdout.WriteLine)

// https://atcoder.jp/contests/typical90/submissions/41599516
