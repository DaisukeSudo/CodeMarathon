// https://atcoder.jp/contests/arc061/tasks/arc061_a

let s = stdin.ReadLine()
[0 .. (1 <<< s.Length - 1) - 1] |> List.map (fun i ->
  ([s.[0 .. 0]], [1 .. s.Length - 1]) ||> List.fold (fun (h :: ts) d ->
    if (i >>> d - 1) &&& 1 = 1
    then s.[d .. d] :: h :: ts
    else (h + s.[d .. d]) :: ts
  )
)
|> Seq.sumBy (Seq.sumBy int64)
|> printfn "%d"

// https://atcoder.jp/contests/arc061/submissions/51732109
