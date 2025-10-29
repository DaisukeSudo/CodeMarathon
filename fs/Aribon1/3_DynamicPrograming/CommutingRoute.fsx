// https://atcoder.jp/contests/joi2007yo/tasks/joi2007yo_f

let a, b = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let n = stdin.ReadLine() |> int
let xy = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

let passable = Array2D.create a b true
for x, y in xy do
  passable.[x, y] <- false

let dp = Array2D.create a b 0 in dp.[0, 0] <- 1

for i in 0 .. a - 1 do
  for j in 0 .. b - 1 do
    if passable.[i, j] then
      for i2, j2 in [(i - 1, j); (i, j - 1)] do
        if i2 >= 0 && j2 >= 0 && passable.[i2, j2] then
          dp.[i, j] <- dp.[i, j] + dp.[i2, j2]

dp.[a - 1, b - 1]
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2007yo/submissions/70539510
