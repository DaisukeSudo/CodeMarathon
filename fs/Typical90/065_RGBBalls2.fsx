// https://atcoder.jp/contests/typical90/tasks/typical90_bm

let p = 998244353L

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

let inverse = fun b -> power b (p - 2L)

let modDiv = fun a b -> a * (inverse b) % p

let permutation =
  fun n r ->
    seq { 0L .. (r - 1L) }
    |> Seq.fold (fun acc i -> acc * (n - i) % p) 1L

let combination =
  fun n r ->
    (permutation n r, permutation r r)
    |> fun (a, b) -> modDiv a b

// ----

let r, g, b, k = stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1], x.[2], x.[3]
let x, y, z    = stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1], x.[2]

if r > 3000L || g > 3000L || b > 3000L then failwith "can't answer"

let ar = [| 0L .. r |] |> Array.map (combination r)
let ag = [| 0L .. g |] |> Array.map (combination g)
let ab = [| 0L .. b |] |> Array.map (combination b)

seq {
  for ri in 0L .. r do
  for gi in 0L .. g do
    let bi = k - ri - gi
    if bi >= 0L && bi <= b
      && ri + gi <= x && gi + bi <= y && bi + ri <= z
    then yield (ri, gi, bi)
}
|> Seq.fold (fun acc (ri, gi, bi) ->
  ar.[int ri]
  |> fun x -> x * ag.[int gi] % p
  |> fun x -> x * ab.[int bi] % p
  |> fun x -> (x + acc) % p
) 0L
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/44887532
