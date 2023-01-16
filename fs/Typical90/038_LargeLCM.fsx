// https://atcoder.jp/contests/typical90/tasks/typical90_al

open FSharp.Core.Operators.Checked

let inline gcd a b = ([| (max a b, min a b) |]) |> fun box -> Seq.initInfinite (fun _ -> box.[0]) |> Seq.takeWhile (fun (_, b) -> b > LanguagePrimitives.GenericZero) |> Seq.iter (fun (a, b) -> box.[0] <- (b, a % b)) |> fun _ -> box.[0] |> fst

let inline lcm a b =
  try
    Ok (a / (gcd a b) * b)
  with
  | :? System.OverflowException as e -> Error e

// ----

let a, b = stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1]

match (lcm a b) with
| Ok(x) when x <= 1_000_000_000_000_000_000L -> x.ToString()
| _ -> "Large"
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/38097266
