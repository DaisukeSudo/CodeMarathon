// https://atcoder.jp/contests/abc104/tasks/abc104_c

stdin.ReadLine().Split() |> fun x -> (int x.[0], int x.[1] / 100) |> fun (d, g) ->
  [| for i in 1..d -> stdin.ReadLine().Split() |> fun x -> (int x.[0], int x.[1] / 100) |] |> fun pcs ->
    [0..(1 <<< d) - 1] |> List.map (fun k ->
      pcs
      |> Array.fold (fun (i, s, l) (p, c) ->
        if (k >>> i) &&& 1 = 1 then (i + 1, (i + 1, p, c) :: s, l) else (i + 1, s, i)
      ) (0, [], -1) |> fun (_, s, l) ->
        s
        |> List.fold (fun (cost, score) (i, p, c) ->
          cost + p, score + i * p + c
        ) (0, 0) |> fun (cost, score) ->
          if score >= g then cost else (g - score |> fun r ->
            if (l + 1) * fst pcs.[l] >= r
            then cost + r / (l + 1) + if r % (l + 1) = 0 then 0 else 1
            else System.Int32.MaxValue
          )
    )
    |> List.min
|> printfn "%d"

// https://atcoder.jp/contests/abc104/submissions/30697729
