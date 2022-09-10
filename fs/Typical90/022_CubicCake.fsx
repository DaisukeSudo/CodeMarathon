// https://atcoder.jp/contests/typical90/tasks/typical90_v

let inline gcd a b = ([| (max a b, min a b) |]) |> fun box -> Seq.initInfinite (fun _ -> box.[0]) |> Seq.takeWhile (fun (_, b) -> b > LanguagePrimitives.GenericZero) |> Seq.iter (fun (a, b) -> box.[0] <- (b, a % b)) |> fun _ -> box.[0] |> fst

// ----

let abc = stdin.ReadLine().Split() |> Array.map int64

let l = abc |> Array.reduce gcd

abc |> Array.sumBy (fun x -> x / l - 1L) |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/34717162
