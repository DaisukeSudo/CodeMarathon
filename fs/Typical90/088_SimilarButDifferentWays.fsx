// https://atcoder.jp/contests/typical90/tasks/typical90_cj

open System.Collections.Generic;;

let n, q = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let a0 = stdin.ReadLine().Split() |> Array.map int
let xy = Array.init q (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])

let a = Array.create 8889 0
for i in 0 .. n - 1 do a.[i] <- a0.[i]

let g = Array.init 8889 (fun _ -> [])
for x, y in xy do g.[x] <- y :: g.[x]

let visited = Array.create 8889 0
let vec = ResizeArray<int>()
let ans = Array.init 8889 (fun _ -> ResizeArray<ResizeArray<int>>())
let mutable continuing = true

let rec dfs pos dep =
  if continuing then
    if pos = (n + 1) then
      ans.[dep].Add (ResizeArray<int>(vec))
      if ans.[dep].Count = 2 then continuing <- false
    else
      // Don't Choose
      dfs (pos + 1) dep

      // Choose
      if visited.[pos] = 0 then
        vec.Add pos
        for i in g.[pos] do visited.[i] <- visited.[i] + 1
        dfs (pos + 1) (dep + a.[pos])
        for i in g.[pos] do visited.[i] <- visited.[i] - 1
        vec.RemoveAt (vec.Count - 1)

dfs 1 0

ans
|> Array.tryFind (fun x -> x.Count > 1)
|> Option.iter (fun x ->
  x.[0] |> Seq.length |> stdout.WriteLine
  x.[0] |> Seq.iter (stdout.WriteLine)
  x.[1] |> Seq.length |> stdout.WriteLine
  x.[1] |> Seq.iter (stdout.WriteLine)
)
