// https://atcoder.jp/contests/arc097/tasks/arc097_b

let unionFindTree n =
  let mutable cnt = n
  let parent = [| 0..n - 1 |]
  let rank = Array.zeroCreate n

  let rec findRoot x =
    if parent.[x] = x then x
    else
      let root = findRoot parent.[x]
      parent.[x] <- root
      root

  let unite a b =
    let rA = findRoot a
    let rB = findRoot b
    if rA <> rB then
      if rank.[rA] < rank.[rB] then
        parent.[rA] <- rB
      elif rank.[rA] > rank.[rB] then
        parent.[rB] <- rA
      else
        parent.[rB] <- rA
        rank.[rA] <- rank.[rA] + 1
      cnt <- cnt - 1

  let same a b = (findRoot a) = (findRoot b)

  (unite, same)

// ----

let n, m = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let p    = stdin.ReadLine().Split() |> Array.map (int >> ((+) -1))
let xy   = Array.init m (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

let unite, same = unionFindTree n

xy
|> Array.iter ((<||) unite)

p
|> Array.mapi same
|> Array.filter id
|> Array.length
|> stdout.WriteLine

// https://atcoder.jp/contests/arc097/submissions/75778271
