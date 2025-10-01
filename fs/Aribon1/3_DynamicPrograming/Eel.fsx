// https://atcoder.jp/contests/tdpc/tasks/tdpc_eel

// P(x) = c0*x^0 + c1*x^1 + c2*x^2 + ⋯ + cK*x^K
let poly (k: int) =
  let MOD = 1000000007L

  let cap (x: int64 []) =
    if x.Length > k + 1 then
      Array.sub x 0 (k + 1)
    else
      x

  let add (a: int64 []) (b: int64 []) =
    let lenA, lenB = a.Length, b.Length
    let lenV = max lenA lenB
    let z = Array.zeroCreate lenV
    for i in 0 .. lenV - 1 do
      let coefA = if i < lenA then a.[i] else 0L
      let coefB = if i < lenB then b.[i] else 0L
      z.[i] <- (coefA + coefB) % MOD
    z |> cap

  let mul (a: int64 []) (b: int64 []) =
    let lenV = min (a.Length + b.Length - 1) (k + 1)
    if lenV <= 0 then [| |]
    else
      let z = Array.zeroCreate lenV
      for i = 0 to a.Length - 1 do
        if a.[i] <> 0L then
          for j = 0 to b.Length - 1 do
            if i + j < lenV && b.[j] <> 0L then
              z.[i + j] <- (z.[i + j] + a.[i] * b.[j]) % MOD
      z |> cap

  let shift (a: int64 []) =
    if a.Length >= k + 1 then [| |]
    else
      let z = Array.zeroCreate (a.Length + 1)
      Array.blit a 0 z 1 a.Length
      z |> cap

  (add, mul, shift)

// ----

let n, k  = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let edges = Array.init (n - 1) (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> (+)(-1)) |> fun x -> x.[0], x.[1])

let g = Array.init n (fun _ -> [])
for node_a, node_b in edges do
    g.[node_a] <- node_b :: g.[node_a]
    g.[node_b] <- node_a :: g.[node_b]

let add, mul, shift = poly k

// ノード u を根とする部分木についての情報
// y0: u の部分木で完結するパスの総和多項式（u が関わるパスの本数 + 1 を適用済）
// y1: u の部分木から u を通って親へ伸びる未完結パスの総和多項式（本数 + 1 は未適用）
let rec dfs u p =
  g.[u]
  |> List.filter (fun v -> v <> p)
  |> List.map (fun v -> dfs v u)
  // x0: u は未使用
  // x1: u は端点
  // x2: u は中点（パスの内部）
  |> List.fold (fun (x0, x1, x2) (y0, y1) ->
    // nx0:
    //   u 元々未使用 (x0) かつ 子 v の部分木も完結 (y0)
    // nx1:
    //   u が元々端点 (x1) かつ 子 v の部分木が完結 (y0)
    //   u が元々未使用 (x0) で 子 v の部分木から u へパスが伸びる (y1)
    // nx2:
    //   u が元々中点 (x2) かつ 子 v の部分木が完結 (y0)
    //   u が元々端点 (x1) で 子 v の部分木から u へパスが伸びて結合する (y1)
    let nx0 = mul x0 y0
    let nx1 = add (mul x1 y0) (mul x0 y1)
    let nx2 = add (mul x2 y0) (mul x1 y1)
    (nx0, nx1, nx2)
  ) (
    // init: u の子からまだ何も来ていない
    //   x0: 0本のパスを選ぶ方法が 1通り
    //   x1, x2: u を端点/中点とするパスはまだない
    [| 1L |], [| |], [| |]
  )
  |> fun (x0, x1, x2) ->
    // y0:
    //   x0: u が未使用であれば，それ以前に選ばれていた完結パスの本数はそのまま
    //   x1 + x2: u を使ったすべてのパスの選び方
    //   mul x^1: u を通るパスを完結させるため，パスの本数を +1 する
    // y1:
    //   x0: u が未使用の状態から u → p へパスを伸ばす
    //   x1: u が端点であれば u → p へ接続を継続する
    //   （X2: u を使ってパスを完結させているため，親へは伸ばせない）
    //   （未確定なのでパスは増やさない）
    let y0 = add x0 (mul (add x1 x2) (shift [| 1L |]))
    let y1 = add x0 x1
    (y0, y1)

dfs 0 -1
|> fst
|> Array.last
|> stdout.WriteLine

// https://atcoder.jp/contests/tdpc/submissions/69770278
