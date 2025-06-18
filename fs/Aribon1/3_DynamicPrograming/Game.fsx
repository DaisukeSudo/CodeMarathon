// https://atcoder.jp/contests/tdpc/tasks/tdpc_game

let readInts () = stdin.ReadLine().Split() |> Array.map int
let n, m = readInts () |> fun x -> x.[0], x.[1] 
let a    = readInts ()
let b    = readInts ()

let dp = Array.init (n + 1) (fun _ -> Array.create (m + 1) -1)
let update i j v =
  dp.[i].[j] <- v
  v

let rec loop i j s =
  if dp.[i].[j] <> -1 then dp.[i].[j]
  else if (i = n && j = m) then 0
  else
    [
      if i = n then 0 else (s - (loop (i + 1) j (s - a.[i])))
      if j = m then 0 else (s - (loop i (j + 1) (s - b.[j])))
    ]
    |> List.max
    |> update i j

[
  a |> Seq.sum
  b |> Seq.sum
]
|> Seq.sum
|> loop 0 0 
|> stdout.WriteLine

// https://atcoder.jp/contests/tdpc/submissions/66867067
