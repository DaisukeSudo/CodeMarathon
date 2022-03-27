// https://atcoder.jp/contests/abc079/tasks/abc079_c

stdin.ReadLine() |> fun s ->
  Array.create (1 <<< 3) (s.[0..0], int s.[0..0]) |> fun arr ->
    [1..3] |> List.map (fun d -> [0..(1 <<< 3) - 1] |> List.iter (fun i ->
      arr.[i] |> fun (w, v) ->
      if (i >>> d - 1) &&& 1 = 1
      then arr.[i] <- (w + "+" + s.[d..d], v + int s.[d..d])
      else arr.[i] <- (w + "-" + s.[d..d], v - int s.[d..d])
    ))
    |> fun _ -> arr |> Array.find (fun (_, v) -> v = 7) |> fun (w, _) -> w + "=7"
|> printfn "%s"

// https://atcoder.jp/contests/abc079/submissions/30505116
