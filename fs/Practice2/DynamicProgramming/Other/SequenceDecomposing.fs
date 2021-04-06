// https://atcoder.jp/contests/abc134/tasks/abc134_e

stdin.ReadLine ()
|> int
|> fun n ->
  (Array.create (n + 1) System.Int32.MaxValue)
  |> fun memo ->
    (List.init n (fun _ -> stdin.ReadLine () |> int))
    |> List.fold (
      fun len x ->
        if memo.[len] >= x
        then
          (memo.[len + 1] <- x)
          |> fun _ -> len + 1
        else
          [1..17]
          |> List.fold (
            fun (li, ri) _ ->
              (li + (ri - li) / 2)
              |> fun mi ->
                if memo.[mi] < x
                then (li, mi)
                else (mi + 1, ri)
          ) (1, len)
          |> fst
          |> fun i -> memo.[i] <- x
          |> fun _ -> len
    ) 0
|> printfn "%d"

// https://atcoder.jp/contests/abc134/submissions/21518984
