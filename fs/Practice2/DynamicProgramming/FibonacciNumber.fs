// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_A

stdin.ReadLine()
|> int
|> fun n ->
  match n with
  | _ when n < 0 -> 0
  | _ when n < 2 -> 1
  | _ ->
    [1..(n - 1)]
    |> List.fold (fun (p1, p2) _ -> (p1 + p2, p1)) (1, 1)
    |> fun (x, _) -> x
|> printfn "%d"
