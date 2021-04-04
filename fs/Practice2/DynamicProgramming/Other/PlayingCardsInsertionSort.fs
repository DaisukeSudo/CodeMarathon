// https://atcoder.jp/contests/abc006/tasks/abc006_4

stdin.ReadLine ()
|> int
|> fun n ->
  (
    List.init n (fun _ -> stdin.ReadLine () |> int),
    Array.create (n + 1) -1
  )
  |> fun (ns, memo) ->
    ns
    |> List.fold (
      fun len x ->
        if memo.[len] < x
        then
          (memo.[len + 1] <- x)
          |> fun _ -> len + 1
        else
          memo
          |> Array.findIndex ((<=) x)
          |> fun i -> memo.[i] <- x
          |> fun _ -> len
    ) 0
  |> fun len -> n - len
|> printfn "%d"

// https://atcoder.jp/contests/abc006/submissions/21492908
