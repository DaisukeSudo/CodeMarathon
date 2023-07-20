// https://atcoder.jp/contests/typical90/tasks/typical90_bj

let n = stdin.ReadLine() |> int
let ab = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

let used = Array.create n false
let q = new System.Collections.Generic.Queue<int>()

// adjacencies
let g = Array.create n [] in
  ab |> Seq.iteri (fun i (a, b) ->
    g.[a] <- i :: g.[a]
    g.[b] <- i :: g.[b]
    if a = i || b = i then
      used.[i] <- true
      q.Enqueue i
  )

let mutable ans = []
while q.Count > 0 do
  let pos = q.Dequeue()
  ans <- pos :: ans
  for i in g.[pos] do
    if not used.[i] then
      used.[i] <- true
      q.Enqueue i

if ans |> List.length <> n then [-1] else ans |> List.map ((+) 1)
|> Seq.iter (stdout.WriteLine)

// https://atcoder.jp/contests/typical90/submissions/43770529
