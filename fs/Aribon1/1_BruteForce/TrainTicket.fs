// https://atcoder.jp/contests/abc079/tasks/abc079_c

stdin.ReadLine() |> fun s ->
  seq {0..(1 <<< s.Length - 1) - 1} |> Seq.map (fun i ->
    [1..s.Length - 1] |> List.fold (fun (w, v) d ->
      if (i >>> d - 1) &&& 1 = 1
      then (w + "+" + s.[d..d], v + int s.[d..d])
      else (w + "-" + s.[d..d], v - int s.[d..d])
    ) (s.[0..0], int s.[0..0])
  )
  |> Seq.find (fun (_, v) -> v = 7) |> fun (w, _) -> w + "=7"
|> printfn "%s"

// https://atcoder.jp/contests/abc079/submissions/30505436
