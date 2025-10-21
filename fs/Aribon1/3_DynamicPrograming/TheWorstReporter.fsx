// https://atcoder.jp/contests/joi2007ho/tasks/joi2007ho_d

let n = stdin.ReadLine() |> int
let m = stdin.ReadLine() |> int
let ij = List.init m (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])

let g = Array.init (n + 1) (fun _ -> [])
let indeg = Array.create (n + 1) 0

for i, j in ij do
  g.[i] <- j :: g.[i]
  indeg.[j] <- indeg.[j] + 1

let q = System.Collections.Generic.Queue<int>()
for i in 1 .. n do
  if indeg.[i] = 0 then q.Enqueue i

let mutable ans = []
let mutable op = false

while q.Count > 0 do
  let u = q.Dequeue()
  ans <- u :: ans
  if q.Count > 0 then op <- true
  for v in g.[u] do
    indeg.[v] <- indeg.[v] - 1
    if indeg.[v] = 0 then q.Enqueue v

ans
|> List.rev
|> List.iter stdout.WriteLine
if op then 1 else 0
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2007ho/submissions/70336636
