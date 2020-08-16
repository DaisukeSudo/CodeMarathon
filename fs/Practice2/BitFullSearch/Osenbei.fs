// https://atcoder.jp/contests/joi2008yo/tasks/joi2008yo_e

stdin.ReadLine()
|> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (r, c) ->
  (
    r,
    c,
    [1..r] |> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map (int >> System.Convert.ToBoolean))
  )
|> fun (r, c, senbeis) ->
  [0..((1 <<< r) - 1)] // 2 ** 10 = 1024
  |> List.map (
    fun crs ->
      senbeis
      |> List.mapi (fun i row -> if crs >>> i &&& 1 = 1 then row |> Array.map not else row) // r = 10
      |> fun rSenbeis ->
        [0..(c - 1)] // 10000
        |> List.sumBy (
          fun i ->
            rSenbeis
            |> List.map (fun row -> row.[i]) // 10
            |> List.fold (fun (a1, a2) x -> if x then (a1 + 1, a2) else (a1, a2 + 1)) (0, 0) // 10
            |> fun (a1, a2) -> max a1 a2
        )
  )
  |> List.max
|> printfn "%d"

// https://atcoder.jp/contests/joi2008yo/submissions/13378025

// 1_024_000_000L
