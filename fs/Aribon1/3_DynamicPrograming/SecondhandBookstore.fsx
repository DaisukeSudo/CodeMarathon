// https://kaage.hatenablog.com/entry/2020/07/23/110706

let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let cg = Array.init n (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1] - 1)

let dp = Array2D.create 11 (k + 1) 0L

let ps = Array.init 10 (fun _ -> []) in
  for (c, g) in cg do
    ps.[g] <- c :: ps.[g]

ps
|> Array.map (Seq.sortDescending >> Seq.toArray)
|> Array.iteri (fun i p ->
  let v = Array.zeroCreate (p.Length + 1)
  // cumulative sum
  for j = 0 to v.Length - 2 do
    v.[j + 1] <- v.[j] + int64 p.[j]
  // add a bonus
  for j = 0 to v.Length - 1 do
    v.[j] <- v.[j] + int64 (j * (j - 1))

  for j in 0 .. k do
    if dp.[i, j] > 0L || j = 0 then
      for m in 0 .. min (v.Length - 1) (k - j) do
        dp.[i + 1, j + m] <- max dp.[i + 1, j + m] (dp.[i, j] + v.[m])
)

dp.[10, k]
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2011ho/submissions/71412033
