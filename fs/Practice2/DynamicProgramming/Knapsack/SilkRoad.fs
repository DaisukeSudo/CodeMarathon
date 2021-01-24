// https://atcoder.jp/contests/joi2015yo/tasks/joi2015yo_d

stdin.ReadLine () |> fun x -> x.Split ' ' |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (n, m) ->
  (
    Array.init n (fun _ -> stdin.ReadLine () |> int) |> Array.append [| 0 |] |> Array.mapi (fun i x -> i, x),
    Array.init m (fun _ -> stdin.ReadLine () |> int) |> Array.append [| 0 |],
    Array.create (m - n + 1) 0
  )
  |> fun (ds, cs, memo0) ->
    ds.[1..]
    |> Array.fold (
      fun memo (i, di) ->
        cs.[i..(i + m - n)]
        |> Array.mapi (
          fun j2 cj -> (di * cj) + (memo.[..j2] |> Array.min)
        )
    ) memo0
  |> Array.min
|> printfn "%d"

// https://atcoder.jp/contests/joi2015yo/submissions/19234289
