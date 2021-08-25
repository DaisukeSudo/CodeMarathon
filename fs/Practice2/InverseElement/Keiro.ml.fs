// https://atcoder.jp/contests/abc034/tasks/abc034_c

let p = 1_000_000_007L

// べき乗
let power =
  ([| fun _ _ -> 0L |])
  |> fun fnc ->
    (
      fun m n ->
        match n with
        | _ when n = 0L -> 1L
        | _ when n % 2L = 0L -> fnc.[0] m (n / 2L) |> (fun x -> x * x % p)
        | _ -> m * fnc.[0] m (n - 1L) % p
    )
    |> fun fn -> (fnc.[0] <- fn) |> fun () -> fn

// 逆元
let inverse = fun b -> power b (p - 2L)

// mod p における割り算
// a / b ≡ a * (1 / b) (mod p)
// 1 / b (mod p) … 逆元
let modDiv = fun a b -> a * (inverse b) % p

// 順列
let permutation = fun n r ->
  seq {0L..(r - 1L)} |> Seq.fold (fun acc i -> acc * (n - i) % p) 1L

// 組合せ
let combination = fun n r ->
  (
    permutation n r,
    permutation r r
  )
  |> fun (a, b) -> modDiv a b

// メイン
(stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1])
|> fun (w, h) -> combination (w + h - 2L) (w - 1L)
|> printfn "%d"

// https://atcoder.jp/contests/abc034/submissions/25330540
