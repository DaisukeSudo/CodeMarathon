// https://atcoder.jp/contests/abc087/tasks/arc090_a

(
    (stdin.ReadLine() |> int),
    (stdin.ReadLine() |> fun x -> ("0 " + x).Split(' ') |> Array.map int),
    (stdin.ReadLine() |> fun x -> ("0 " + x).Split(' ') |> Array.map int)
)
|> fun (n, a1, a2) ->
    [1..n]
    |> Seq.fold (fun (upperTotal, lowerTotal) i ->
        upperTotal + a1.[i]
        |> fun nextUpperTotal ->
        (
            nextUpperTotal,
            (max nextUpperTotal lowerTotal) + a2.[i]
        )
    ) (0, 0)
    |> fun (_, ret) -> ret
|> printfn "%d"

// https://atcoder.jp/contests/abc087/submissions/10486616
