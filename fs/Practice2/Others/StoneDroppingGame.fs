// https://atcoder.jp/contests/s8pc-3/tasks/s8pc_3_b

stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1], x.[2])
|> fun (h, w, k) ->
  [|0..h - 1|] |> Array.map (fun _ -> [stdin.ReadLine() |> Seq.map (string >> int >> Some) |> Seq.toArray; [| None |]] |> Array.concat)
  |> fun initialState -> (fun () -> Array.init h (fun i -> initialState.[i].[0..]))
  |> fun newState ->
    seq { for x in 1..h - 1 do for y in 0..w - 1 -> (x, y) } |> Seq.map (fun (x, y) ->
      newState ()
      |> fun state -> (state.[x].[y] <- None) |> fun () -> ([| true |], [| 0 |]) |> fun (continued, score) ->
        seq { 0..h - 1 }
        |> Seq.takeWhile (fun _ -> continued.[0])
        |> Seq.iter (fun i ->
          [
            continued.[0] <- false;
            (* dropping *)
            seq { for x in h - 1..(-1)..1 do for y in w - 1..(-1)..0 -> (x, y) } |> Seq.iter (fun (x, y) ->
              if state.[x].[y] = None then (
                [| None |]
                |> fun above ->
                  seq { x - 1..(-1)..0 }
                  |> Seq.takeWhile (fun _ -> above.[0] = None)
                  |> Seq.iter (fun x2 ->
                    if state.[x2].[y] <> None
                    then (
                      (above.[0] <- state.[x2].[y])
                      |> fun () -> state.[x2].[y] <- None
                    )
                  )
                  |> fun () -> state.[x].[y] <- above.[0]
              )
            );
            (* erasing *)
            [1..h - 1] |> Seq.iter (fun x ->
              [0..w] |> Seq.fold (fun (prev, chain) y ->
                state.[x].[y]
                |> fun cur ->
                  if cur = prev && cur <> None
                  then (cur, chain + 1)
                  else (
                    if chain >= k
                    then (
                      (
                        2 |> Array.create i |> Array.fold ( * ) 1,
                        prev |> Option.get |> ( * ) chain
                      )
                      |> fun (a, b) -> score.[0] <- score.[0] + (a * b)
                      |> fun () -> [y - 1..(-1)..y - chain] |> Seq.iter (fun y2 -> state.[x].[y2] <- None)
                      |> fun () -> (continued.[0] <- true)
                    )
                    |> fun () -> (cur, 1)
                  )
              ) (None, 1)
              |> ignore
            );
          ]
          |> ignore
        )
        |> fun () -> score.[0]
    )
    |> Seq.max
|> printfn "%d"

// https://atcoder.jp/contests/s8pc-3/submissions/29381891
