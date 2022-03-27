// https://atcoder.jp/contests/arc061/tasks/arc061_a

stdin.ReadLine() |> fun s ->
  Array.create (1 <<< s.Length - 1) [s.[0..0]] |> fun arr ->
    [1..s.Length - 1] |> List.map (fun d ->
      [0..(1 <<< s.Length - 1) - 1] |> List.iter (fun i ->
        match arr.[i] with
        | (h :: ts) ->
          if (i >>> d - 1) &&& 1 = 1
          then arr.[i] <- s.[d..d] :: h :: ts
          else arr.[i] <- (h + s.[d..d]) :: ts
        | _ -> ()
      )
    )
    |> fun _ -> arr |> Array.sumBy (Seq.sumBy int64)
|> printfn "%d"

// https://atcoder.jp/contests/arc061/submissions/30504294
