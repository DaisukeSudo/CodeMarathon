// https://atcoder.jp/contests/joi2008ho/tasks/joi2008ho_a

[|[]|]
|> fun line ->
  stdin.ReadLine() |> int
  |> fun n -> [1..n]
  |> List.iter (fun i ->
    stdin.ReadLine()
    |> fun x ->
      if i % 2 = 1
      then (
        match line.[0] with
        | (y, size)::ts when x = y -> line.[0] <- (x, size + 1)::ts
        | _ -> line.[0] <- (x, 1)::line.[0]
      )
      else (
        match line.[0] with
        | (y, size)::ts when x = y -> line.[0] <- (x, size + 1)::ts
        | (_, size1)::ts1 ->
          match ts1 with
          | (_, size2)::ts2 -> line.[0] <- (x, size1 + size2 + 1)::ts2
          | _ -> line.[0] <- (x, size1 + 1)::[]
        | _ -> ()
      )
  )
  |> fun () -> line.[0]
  |> List.filter (fun (x, _) -> x = "0")
  |> List.sumBy (fun (_, size) -> size)
|> printfn "%d"

// https://atcoder.jp/contests/joi2008ho/submissions/28712955
