// https://atcoder.jp/contests/typical90/tasks/typical90_b

stdin.ReadLine() |> int |> fun n ->
  if n &&& 1 <> 1 then
    ([|[("(", 1)]|], [|[]|]) |> fun (stack, ans) ->
      Seq.initInfinite (fun _ ->
        match stack.[0] with
        | [] -> ()
        | (s, l) :: ts ->
          stack.[0] <- ts
          if s.Length = n
          then ans.[0] <- s :: ans.[0]
          else (
            if l > 0
            then (stack.[0] <- (s + ")", l - 1) :: stack.[0])
            if n - s.Length - l > 0
            then (stack.[0] <- (s + "(", l + 1) :: stack.[0])
          )
      )
      |> Seq.takeWhile (fun _ -> stack.[0] |> List.length > 0)
      |> Seq.length
      |> fun _ -> ans.[0] |> List.rev |> String.concat "\n"
      |> printfn "%s"

// https://atcoder.jp/contests/typical90/submissions/30920758
