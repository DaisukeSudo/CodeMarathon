// https://atcoder.jp/contests/abc098/tasks/arc098_a

(
    (stdin.ReadLine() |> int),
    (stdin.ReadLine() |> Seq.map string |> Seq.toList)
)
|> fun (n, s) ->
  s.[1..(n - 1)]
  |> Seq.fold (fun (prev, prevRet, ret) cur ->
    (
        (if prev = "E" then  0 else 1),
        (if cur  = "E" then -1 else 0)
    )
    |> (fun (a, b) -> prevRet + a + b)
    |> (fun newPrevRet ->
        (cur, newPrevRet, min ret newPrevRet)
    )
  ) (
    s.[1..(n - 1)]
    |> Seq.filter (fun x -> x = "E") 
    |> Seq.length
    |> fun x -> (s.[0], x, x)
  )
  |> (fun (_, _, ret) -> ret)
  |> printfn "%d"

// https://atcoder.jp/contests/abc098/submissions/10724350
