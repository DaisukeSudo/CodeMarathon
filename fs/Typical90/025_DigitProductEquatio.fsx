// https://atcoder.jp/contests/typical90/tasks/typical90_y

// m - f(m) = b

// let f x =
//   seq {1 .. x.ToString().Length}
//   |> Seq.fold (fun (x, a) _ -> (x / 10, x % 10 * a)) (x, 1)
//   |> snd

// [1..1000] |> List.groupBy f |> List.sortBy fst |> List.iter (printfn "%A")

// ----

let n, b = stdin.ReadLine().Split() |> fun x -> int64 x.[0], int64 x.[1]
