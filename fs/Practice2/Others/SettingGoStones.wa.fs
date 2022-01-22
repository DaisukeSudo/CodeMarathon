// https://atcoder.jp/contests/joi2008ho/tasks/joi2008ho_a

stdin.ReadLine() |> int
|> fun n ->
  seq {1..n}
  |> Seq.map (fun _ -> stdin.ReadLine() |> fun x -> if x = "0" then 1 else 0)
  |> Seq.rev
  |> Seq.toList
  |> fun gs ->
    gs
    |> List.tail
    |> List.fold (fun (prev, even, continued, total) cur ->
      if even || continued
      then
        (prev, not even, prev <> cur, total + prev)
      else
        (cur, not even, false, total + cur)
    ) (
      gs |> List.head,
      n % 2 = 0,
      false,
      gs |> List.head
    )
  |> fun (_, _, _, x) -> x
|> printfn "%d"
