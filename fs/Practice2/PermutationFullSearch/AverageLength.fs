// https://atcoder.jp/contests/abc145/tasks/abc145_c

stdin.ReadLine()
|> int
|> fun n ->
  (
    n |> float,
    [1..n] |> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map float |> fun x -> (x.[0], x.[1]))
  )
|> fun (n, ps) ->
  ps
  |> List.fold (
    fun (ps, ds) (x1, y1) ->
      match ps with
      | [] -> ([], ds)
      | _ ->
        (
          ps |> List.tail,
          ds @ (ps |> List.map (fun (x2, y2) -> ((x2 - x1) ** 2. + (y2 - y1) ** 2.) |> sqrt))
        )
  ) (ps |> List.tail, [])
  |> fun (_, ds) -> ds |> List.average
  |> fun avg -> avg * (n - 1.)
|> printfn "%A"

// https://atcoder.jp/contests/abc145/submissions/14278179
