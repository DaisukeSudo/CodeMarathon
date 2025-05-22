// https://atcoder.jp/contests/abc110/tasks/abc110_d

let factorize n =
  let mutable x = n
  let mutable ans = []
  let z = int64 (sqrt (float n)) + 1L
  for i in 2L :: [3L .. 2L .. z] do
    let rec loop () =
      if x % i = 0L then
        x <- x / i
        ans <- i :: ans
        loop ()
    loop ()
  if x <> 1L then ans <- x :: ans
  ans

let p = 1_000_000_007L

let power =
  let rec loop m n =
    match n with
    | _ when n = 0L -> 1L
    | _ when n % 2L = 0L -> loop m (n / 2L) |> (fun x -> x * x % p)
    | _ -> m * loop m (n - 1L) % p
  loop

let inverse b = power b (p - 2L)

let modDiv a b = a * (inverse b) % p

let permutation n r = seq { 0L .. (r - 1L) } |> Seq.fold (fun acc i -> acc * (n - i) % p) 1L

let combination n r = (permutation n r, permutation r r) ||> modDiv

// ----

let solve n m =
  m
  |> factorize
  |> List.countBy id
  |> List.fold (fun acc (_, e) ->
    (combination (n + int64 e - 1L) (int64 e)) * acc % p
  ) 1L

stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1]
||> solve
|> stdout.WriteLine

// https://atcoder.jp/contests/abc110/submissions/66032829
