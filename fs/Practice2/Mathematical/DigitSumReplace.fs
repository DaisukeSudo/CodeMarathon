// https://atcoder.jp/contests/ddcc2020-qual/tasks/ddcc2020_qual_d

stdin.ReadLine() |> int |> fun m -> seq {1..m}
|> Seq.map (fun _ -> stdin.ReadLine().Split() |> (fun x -> int64 x.[0], int64 x.[1]))
|> Seq.fold (fun (total, prev) (d, c) ->
  (
    c - 1L + c * d / 10L,
    d * c % 9L
  )
  |> fun (subtotal, curr) ->
    (
      total + subtotal + (prev + curr + 10L) / 10L,
      prev + curr % 9L
    )
) (-1L, 0L)
|> fst
|> printfn "%d"

// https://atcoder.jp/contests/ddcc2020-qual/submissions/30369156
