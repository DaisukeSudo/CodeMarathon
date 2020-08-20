// https://atcoder.jp/contests/joi2008ho/tasks/joi2008ho_c

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1])
|> fun (n, m) ->
  [1..n]
  |> List.map (fun _ -> stdin.ReadLine() |> int)
  |> fun ns -> 0::ns |> List.toArray
  |> fun ns ->
    ns
    |> Array.collect (fun x1 -> ns |> Array.map (fun x2 -> x1 + x2))
    |> Array.distinct
    |> Array.sort
  |> fun ns2 ->
    ns2
    |> Array.fold(
      fun acc p2 ->
        [1..20]
        |> List.fold (
          fun (li, ri) _ ->
            li + (ri - li + 1) / 2
            |> fun mi ->
              if p2 + ns2.[mi] <= m
              then (mi, ri)
              else (li, mi - 1)
        ) (0, ns2.Length - 1)
        |> fun (_, i) ->
          if i < 0
          then 0
          else p2 + ns2.[i]
        |> max acc 
    ) 0
|> printfn "%A"

// https://atcoder.jp/contests/joi2008ho/submissions/16060159
