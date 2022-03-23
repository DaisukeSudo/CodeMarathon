// https://atcoder.jp/contests/ddcc2020-qual/tasks/ddcc2020_qual_d

stdin.ReadLine() |> int |> fun m -> seq {1..m}
|> Seq.map (fun _ -> stdin.ReadLine().Split() |> (fun x -> int64 x.[0], int64 x.[1]))
|> Seq.fold (fun (digit, total) (d, c) -> (digit + c, total + d * c)) (-1L, -1L)
|> fun (digit, total) -> digit + total / 9L
|> printfn "%d"

// https://atcoder.jp/contests/ddcc2020-qual/submissions/30373665
