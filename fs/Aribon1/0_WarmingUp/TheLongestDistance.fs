// https://atcoder.jp/contests/arc004/tasks/arc004_1

// 3
// 1 1
// 2 4
// 4 3

// ----

stdin.ReadLine()
|> int
|> fun x -> [1..x]
|> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map float |> fun x -> (x.[0], x.[1]))
|> Seq.sortBy (fun (x, _)-> x |> int)
|> Seq.mapi (fun i (x, y) -> (i, x, y))
|> Seq.toList
|> fun list -> 
  list
  |> List.fold (fun acc (i, x1, y1) ->
    list
    |> List.filter (fun (j, _, _) -> j > i)
    |> List.map    (fun (_, x2, y2) -> ((x2 - x1) ** 2. + (y2 - y1) ** 2.) |> sqrt)
    |> fun d -> if d.IsEmpty then acc else d |> List.max |> max acc
  ) 0.
|> printfn "%f"

// https://atcoder.jp/contests/arc004/submissions/11464533
