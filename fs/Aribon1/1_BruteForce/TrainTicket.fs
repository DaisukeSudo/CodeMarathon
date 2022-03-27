// https://atcoder.jp/contests/abc079/tasks/abc079_c

stdin.ReadLine() |> Seq.map string |> Seq.toArray |> fun s ->
  seq {0..(1 <<< s.Length - 1) - 1} |> Seq.map (fun i ->
    [1..s.Length - 1] |> Seq.fold (fun (w, v) d ->
      if (i >>> d - 1) &&& 1 = 1
      then (w + "+" + s.[d], v + int s.[d])
      else (w + "-" + s.[d], v - int s.[d])
    ) (s.[0], int s.[0])
  )
  |> Seq.find (fun (_, v) -> v = 7) |> fun (w, _) -> w + "=7"
|> printfn "%s"

// https://atcoder.jp/contests/abc079/submissions/30505879
