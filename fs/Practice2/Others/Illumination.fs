// https://atcoder.jp/contests/joi2013ho/tasks/joi2013ho1

[|[]|]
|> fun line ->
  stdin.ReadLine()
  |> fun _ -> stdin.ReadLine().Split()
  |> Array.iter (fun x ->
    match line.[0] with
    | (y, size)::ts when x <> y -> line.[0] <- (x, size + 1)::ts
    | _ -> line.[0] <- (x, 1)::line.[0]
  )
  |> fun () ->
    line.[0]
    |> List.fold (fun (x1, x2, acc) (_, x0) ->
      (x0, x1, max acc (x0 + x1 + x2))
    ) (0, 0, 0)
    |> fun (_, _, acc) -> acc
|> printfn "%A"

// https://atcoder.jp/contests/joi2013ho/submissions/28716389
