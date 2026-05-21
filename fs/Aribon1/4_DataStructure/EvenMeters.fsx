// https://atcoder.jp/contests/arc036/tasks/arc036_d

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

let n, q = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let wxyz = Array.init q (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1], x.[2], x.[3])

let unite, same = unionFindTree (2 * n + 3)

let pave x y z =
  if z % 2 = 0 then
    unite (2 * x) (2 * y)
    unite (2 * x + 1) (2 * y + 1)
  else
    unite (2 * x) (2 * y + 1)
    unite (2 * x + 1) (2 * y)

let mutable ans = []
let query x y =
  ans <- same (2 * x) (2 * y) :: ans

wxyz
|> Array.iter (fun (w, x, y, z) ->
  if w = 1
  then pave x y z
  else query x y
)

ans
|> List.rev
|> List.map (fun a -> if a then "YES" else "NO")
|> List.iter (stdout.WriteLine)

// https://atcoder.jp/contests/arc036/submissions/75980968
