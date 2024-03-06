// https://atcoder.jp/contests/typical90/tasks/typical90_cl

let n, k = stdin.ReadLine().Split() |> fun x -> int64 x.[0], int x.[1] in if k > 1 || n > 100000L then failwith "give up"
let p = 998244353L

seq { 1L .. n }
|> Seq.fold (fun (a0, a1) _ -> ((a0 + a1) % p, a0)) (1L, 0L)
|> fun (a0, a1) -> (a0 + a1) % p
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/50953916
