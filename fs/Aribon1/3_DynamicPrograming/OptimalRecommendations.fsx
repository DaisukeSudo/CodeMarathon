// https://atcoder.jp/contests/indeednow-finala-open/tasks/indeednow_2015_finala_c

let n, m = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let abcw = Array.init n (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int x.[2], int64 x.[3])
let xyz  = Array.init m (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int x.[2])

let dp = Array3D.create 101 101 101 0L in
  abcw |> Seq.iter (fun (a, b, c, w) -> dp.[a, b, c] <- max dp.[a, b, c] w)

seq {
  for a in 0 .. 100 do
  for b in 0 .. 100 do
  for c in 0 .. 100 do
  yield (a, b, c)
}
|> Seq.iter (fun (a, b, c) ->
  if a > 0 then dp.[a, b, c] <- max dp.[a, b, c] dp.[a - 1, b, c]
  if b > 0 then dp.[a, b, c] <- max dp.[a, b, c] dp.[a, b - 1, c]
  if c > 0 then dp.[a, b, c] <- max dp.[a, b, c] dp.[a, b, c - 1]
)

xyz
|> Seq.map (fun (xi, yi, zi) -> dp.[xi, yi, zi])
|> Seq.iter stdout.WriteLine

// https://atcoder.jp/contests/indeednow-finala-open/submissions/59718620
