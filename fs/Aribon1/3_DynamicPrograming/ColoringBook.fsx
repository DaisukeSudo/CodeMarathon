// https://atcoder.jp/contests/abc036/tasks/abc036_d

let n  = stdin.ReadLine() |> int
let ab = Array.init (n - 1) (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1])

let p = 1_000_000_007L

let g = Array.create (n + 1) List.empty
for (a, b) in ab do
  g.[a] <- b :: g.[a]
  g.[b] <- a :: g.[b]

// dp.[v] = (W, B)
let dp = Array.create (n + 1) (0L, 0L)
let visited = Array.create (n + 1) false

let rec dfs (v: int) =
  visited.[v] <- true
  let mutable wc = 1L
  let mutable bc = 1L

  for u in g.[v] do
    if not visited.[u] then
      dfs u |> ignore

      match dp.[u] with
      | (a, b) ->
        // if v = W then W or B
        wc <- (wc * (a + b) % p)
        // if v = B then W
        bc <- (bc * a % p)
  
  dp.[v] <- (wc, bc)
  (wc, bc)

dfs 1
|> fun (w, b) -> (w + b) % p
|> stdout.WriteLine

// https://atcoder.jp/contests/abc036/submissions/69026417
