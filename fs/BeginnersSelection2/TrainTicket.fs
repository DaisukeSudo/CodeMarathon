// https://atcoder.jp/contests/abc079/tasks/abc079_c

stdin.ReadLine()
|> fun x ->
  x
  |> Seq.map (string >> int)
  |> Seq.toArray
  |> fun x -> (x.[0], x.[1], x.[2], x.[3])
  |> fun (a, b, c, d) ->
  (
    match (a, b, c, d) with
    | _ when a + b + c + d = 7 -> sprintf "%d+%d+%d+%d=7" a b c d
    | _ when a + b + c - d = 7 -> sprintf "%d+%d+%d-%d=7" a b c d
    | _ when a + b - c + d = 7 -> sprintf "%d+%d-%d+%d=7" a b c d
    | _ when a + b - c - d = 7 -> sprintf "%d+%d-%d-%d=7" a b c d
    | _ when a - b + c + d = 7 -> sprintf "%d-%d+%d+%d=7" a b c d
    | _ when a - b + c - d = 7 -> sprintf "%d-%d+%d-%d=7" a b c d
    | _ when a - b - c + d = 7 -> sprintf "%d-%d-%d+%d=7" a b c d
    | _ when a - b - c - d = 7 -> sprintf "%d-%d-%d-%d=7" a b c d
    | _ -> ""
  )
|> printfn "%s"

// https://atcoder.jp/contests/abc079/submissions/10934845
