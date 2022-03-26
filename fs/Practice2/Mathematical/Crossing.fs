// https://atcoder.jp/contests/tenka1-2018-beginner/tasks/tenka1_2018_d

stdin.ReadLine() |> int
|> fun n -> 
  seq {1..448}
  |> Seq.map (fun k -> k * (k - 1) / 2)
  |> Seq.tryFindIndex ((=) n)
  |> fun t ->
    match t with
    | Some(_) ->
      (Array.create 448 [])
      |> fun sgs ->
        [1..n]
        |> List.fold (fun (i, j) x ->
          [
            sgs.[i]     <- x :: sgs.[i]
            sgs.[j % i] <- x :: sgs.[j % i]
          ]
          |> fun _ ->
            if j < i then (i, j + 1) else (i + 1, 1)
        ) (1, 1)
        |> fun (k, _) ->
          sgs.[0..k - 1]
          |> Array.map (
            List.map string >> String.concat " " >> sprintf "%d %s" (k - 1)
          )
          |> String.concat "\n"
          |> sprintf "Yes\n%d\n%s" k
    | None -> "No" 
|> printfn "%s"

// https://atcoder.jp/contests/tenka1-2018-beginner/submissions/30425834
