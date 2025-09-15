// https://atcoder.jp/contests/tdpc/tasks/tdpc_tree

let p = 1_000_000_007L

let factorials n: int64 [] =
  let f = Array.create (n + 1) 1L
  for i in 1 .. n do f.[i] <- f.[i - 1] * int64 i % p
  f

let power =
  let rec loop m n =
    match n with
    | _ when n = 0L -> 1L
    | _ when n % 2L = 0L -> loop m (n / 2L) |> (fun x -> x * x % p)
    | _ -> m * loop m (n - 1L) % p
  loop

let inverse (b: int64): int64 =
  power b (p - 2L) % p

let inverses (f: int64 []): int64 [] =
  let n = f.Length - 1
  let inv = Array.create (n + 1) 1L
  inv.[n] <- inverse f.[n]
  for i in n - 1 .. -1 .. 0 do
    inv.[i] <- inv.[i + 1] * int64 (i + 1) % p
  inv

let makeCombination n =
  let fact = factorials n
  let inv  = inverses fact
  let combination m r =
    if m < r || r < 0 || m > n then 0L 
    else fact.[m] * inv.[r] % p * inv.[m - r] % p
  combination

// ----

let n  = stdin.ReadLine() |> int
let ab = List.init (n - 1) (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> (+)(-1)) |> fun x -> x.[0], x.[1])

let g = Array.init n (fun _ -> []) in
  for (a, b) in ab do
    g.[a] <- b :: g.[a]
    g.[b] <- a :: g.[b]

let comb = makeCombination n
let inv2 = inverse 2L

// W = その部分木の描き方の数（通り数）
// S = 部分木に含まれる辺の数
let rec dfs i parent =
  ((1L, 0), g.[i]) ||> List.fold (fun (accW, accS) j ->
    if j = parent then
      (accW, accS)
    else
      let childW, childS = dfs j i
      let interleave = comb (accS + childS) childS // accS 個と childS 個を並べるときに childS の置き場所を選ぶ組合せ数
      let newW = accW * childW % p * (int64 interleave) % p
      let newS = accS + childS
      (newW, newS)
  )
  |> fun (w, s) -> (w, s + 1)

seq { 0 .. n - 1 }
|> Seq.map (fun i -> (dfs i -1) |> fst)
|> Seq.sum
|> fun x -> x % p * inv2 % p
|> stdout.WriteLine

// https://atcoder.jp/contests/tdpc/submissions/69373437
