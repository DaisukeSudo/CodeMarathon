// https://atcoder.jp/contests/abc021/tasks/abc021_d

1_000_000_007L
|> fun p -> ((([| fun _ _ -> 0L |]) |> fun fnc -> (fun m n -> match n with | _ when n = 0L -> 1L | _ when n % 2L = 0L -> fnc.[0] m (n / 2L) |> (fun x -> x * x % p) | _ -> m * fnc.[0] m (n - 1L) % p) |> fun fn -> (fnc.[0] <- fn) |> fun () -> fn), (fun n r -> seq {0L..(r - 1L)} |> Seq.fold (fun acc i -> acc * (n - i) % p) 1L)) |> fun (power, permutation) -> (fun n r -> (permutation n r, permutation r r) |> fun (a, b) -> a * (power b (p - 2L)) % p)
|> fun combination -> 
  (stdin.ReadLine() |> int64, stdin.ReadLine() |> int64)
  |> fun (n, k) -> combination (n - 1L + k) k
|> printfn "%d"

// https://atcoder.jp/contests/abc021/submissions/25827240
