// https://atcoder.jp/contests/abc070/tasks/abc070_d

let readInt   () = stdin.ReadLine() |> int
let readInts  () = stdin.ReadLine().Split() |> Array.map int
let readInts2 () = readInts () |> fun x -> x.[0], x.[1]
let readInts3 () = readInts () |> fun x -> x.[0], x.[1], x.[2]

let n    = readInt()
let es   = List.init (n - 1) (ignore >> readInts3)
let q, k = readInts2()
let qs   = List.init q (ignore >> readInts2)

let g = Array.create (n + 1) [] in
  for (a, b, c) in es do
    g.[a] <- (b, c) :: g.[a]
    g.[b] <- (a, c) :: g.[b]

let rec dfs i p cost (d: Map<int, int64>) =
  (d.Add(i, cost), g.[i])
  ||> List.fold (fun acc (j, c) ->
    if j = p then
      acc
    else
      acc |> dfs j i (cost + int64 c)
  )

let d = dfs k -1 0L Map.empty

qs
|> List.map (fun (x, y) -> d.[x] + d.[y])
|> List.iter stdout.WriteLine

// https://atcoder.jp/contests/abc070/submissions/69367829
