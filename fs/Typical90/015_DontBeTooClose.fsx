// https://atcoder.jp/contests/typical90/tasks/typical90_o

let p = 1_000_000_007L

let rec power m n =
  match n with
  | _ when n = 0L -> 1L
  | _ when n % 2L = 0L ->
    let x = power m (n / 2L)
    x * x % p
  | _ ->
    let x = power m (n - 1L)
    m * x % p

let inline inverse b = power b (p - 2L)

let inline div a b = a * (inverse b) % p

// ----

let fact = (1L, [|1L..100001L|]) ||> Array.scan (fun a i -> a * i % p)
let factInv = [|0..100000|] |> Array.map (fun i -> div 1L fact.[i])

let inline ncr n r =
  if n < r || r < 0 then 0L else
    (fact.[n] * factInv.[r] % p) * factInv.[n - r] % p

let inline query n k =
  (0L, seq { 1..n / k + 1 }) ||> Seq.fold (fun a i ->
    let s1 = n - (k - 1) * (i - 1)
    let s2 = i
    (a + ncr s1 s2) % p
  )

// ----

let n = stdin.ReadLine() |> int

seq {1..n}
|> Seq.map (query n)
|> Seq.iter stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/33185785
