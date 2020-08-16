// https://atcoder.jp/contests/arc054/tasks/arc054_b

stdin.ReadLine()
|> decimal
|> fun p ->
  (
    fun x -> (float x) + (float p) / 2. ** ((float x) / 1.5)
  )
  |> fun fn ->
    [1..200]
    |> List.fold (
      fun (lv, rv) _ ->
        (
          lv + (rv - lv) / 3M,
          lv + (rv - lv) / 3M * 2M
        )
        |> fun (mv1, mv2) ->
          if (fn mv1 < fn mv2)
          then (lv, mv2)
          else (mv1, rv)
    ) (0M, p)
    |> fun (_, v) -> fn v |> decimal
|> printfn "%M"

// https://atcoder.jp/contests/arc054/submissions/15982457
