// https://atcoder.jp/contests/abc065/tasks/abc065_b

stdin.ReadLine()
|> int
|> (fun n ->
    [1..n]
    |> List.map (fun _ -> stdin.ReadLine() |> int)
    |> fun list -> 0::list
    |> List.toArray
)
|> (fun list ->
    list
    |> Array.fold (
        fun (acc, i, found) _ ->
            match list.[i] with
            | 2 when found = -1 -> (acc + 1, 2, acc)
            | ai -> (acc + 1, ai, found)
    ) (1, 1, -1)
    |> fun (_, _, found) -> found
)
|> printfn "%d"

// https://atcoder.jp/contests/abc065/submissions/10176112
