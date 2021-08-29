// https://atcoder.jp/contests/abc145/tasks/abc145_d

let p = 1_000_000_007L

let power = ([| fun _ _ -> 0L |]) |> fun fnc -> (fun m n -> match n with | _ when n = 0L -> 1L | _ when n % 2L = 0L -> fnc.[0] m (n / 2L) |> (fun x -> x * x % p) | _ -> m * fnc.[0] m (n - 1L) % p) |> fun fn -> (fnc.[0] <- fn) |> fun () -> fn

let inverse = fun b -> power b (p - 2L)

let division = fun a b -> a * (inverse b) % p

let permutation = fun n r -> seq {0L..(r - 1L)} |> Seq.fold (fun acc i -> acc * (n - i) % p) 1L

let combination = fun n r -> (permutation n r, permutation r r) |> fun (a, b) -> division a b

(stdin.ReadLine().Split() |> Array.map int64 |> Array.sort |> fun x -> x.[0], x.[1])
|> fun (x, y) ->
  if (x + y) % 3L <> 0L || x * 2L < y
  then 0L
  else combination ((x + y) / 3L) ((x * 2L - y) / 3L)
|> printfn "%d"

// ----------
// 前提条件
// x <= y （逆にしても答えは変わらないのでソートする）
//
// 総移動回数
// (x + y) / 3
//
// 未達条件
// (x + y) が 3 で割り切れない または x * 2 < y
//
// すべて (i + 2, j + 1) を選択したときとの距離の差
// a = x - (y / 2)
//
// 1回だけ (i + 1, j + 2) を選択する場合を考える
// x = 4, y = 5
// a = 4 - (5 / 2) = 1.5
// 本来 (i + 1, j + 2) を選択すべき回数の差 1 に対して 1.5 ずつ距離がズレることが分かる
//
// よって a を 1.5 で割れば，(i + 1, j + 2) を選択すべき回数がわかる
// b = a / 1.5 = a * 2 / 3 = (x - (y / 2)) * 2 / 3
// = (x * 2 - y) / 3
//
// 総移動回数から前述の値を引けば，(i + 2, j + 1) を選択すべき回数が分かる
// (i + 1, j + 2) の選択回数: b
// (i + 2, j + 1) の選択回数: (x + y) / 3 - b
//
// ----------

// https://atcoder.jp/contests/abc145/submissions/25398772
