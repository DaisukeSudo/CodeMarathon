// https://atcoder.jp/contests/typical90/tasks/typical90_ce

let n, m = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let ab = Array.init m (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0] - 1, x.[1] - 1)
let q = stdin.ReadLine() |> int
let x, y = Array.init q (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0] - 1, x.[1]) |> Array.unzip

// adjacent nodes
let g = Array.create n [] in
  for a, b in ab do 
    g.[a] <- b :: g.[a]
    g.[b] <- a :: g.[b]

// nodes with many adjacencies
let b = 2 * m |> float |> sqrt |> int
let mutable large = [] in
  for i in 0 .. n - 1 do
    if g.[i] |> List.length >= b then
      large <- i :: large

// ...
let largeSize = large |> List.length
let link = Array.init n (fun _ -> Array.create n false) in
  for i in 0 .. largeSize - 1 do
    for j in g.[large.[i]] do
      link.[i].[j] <- true
    link.[large.[i]].[i] <- true

// ...
let mutable ans = []
let update = Array.create n -1
let updateLarge = Array.create largeSize -1
for i in 0 .. q - 1 do
  let mutable last = update.[x.[i]]
  for j in 0 .. largeSize - 1 do
    if link.[x.[i]].[j] then
      last <- max last updateLarge.[j]

  if g.[x.[i]].Length < b then
    update.[x.[i]] <- i
    for j in g.[x.[i]] do
      update.[j] <- i
  else
    let ptr = large |> List.findIndex ((=) x.[i])
    updateLarge.[ptr] <- i

  ans <- (if last = -1 then 1 else y.[last]) :: ans

// ...
ans |> List.rev |> List.iter stdout.WriteLine
