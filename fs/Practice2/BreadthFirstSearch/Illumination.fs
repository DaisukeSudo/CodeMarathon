// https://atcoder.jp/contests/joi2012yo/tasks/joi2012yo_e

stdin.ReadLine()
|> fun x -> x.Split()
|> Array.map int
|> fun x -> (x.[0], x.[1])
|> fun (w, h) ->
  Array.init h (fun _ -> stdin.ReadLine() |> fun x -> x.Split() |> Array.map ((=) "1"))
  |> fun (buildings: bool [] []) ->
    Array.concat [ [| Array.create w false |]; buildings; [| Array.create w false |] ]
    |> Array.map (fun row -> Array.concat [ [| false |]; row; [| false |] ])
  |> fun (buildings: bool [] []) ->
    buildings
    |> Array.map (Array.map id)
    |> fun (touched: bool [] []) ->
      (touched.[0].[0] <- true)
      |> fun () ->
        (
          [| [ (0, 0) ] |],
          [| 0 |]
        )
      |> fun (queue: (int * int) list array, total: int array) ->
        Seq.initInfinite (
          fun _ ->
            match queue.[0] with
            | [] -> ()
            | (i, j)::ts ->
              (if i &&& 1 = 1 then 1 else -1)
              |> fun d ->
                [
                  (i, j + 1);
                  (i, j - 1);
                  (i - 1, j);
                  (i - 1, j + d);
                  (i + 1, j);
                  (i + 1, j + d);
                ]
                |> List.filter (fun (i2, j2) -> 0 <= i2 && i2 <= h + 1 && 0 <= j2 && j2 <= w + 1)
              |> fun neighbors ->
                neighbors
                |> List.filter (fun (i2, j2) -> buildings.[i2].[j2])
                |> List.length
                |> fun count -> (total.[0] <- total.[0] + count)
                |> fun () ->
                  neighbors
                  |> List.filter (fun (i2, j2) -> not touched.[i2].[j2])
                |> List.map (fun (i2, j2) -> (touched.[i2].[j2] <- true) |> fun () -> (i2, j2))
                |> fun ts2 -> queue.[0] <- ts @ ts2
        )
        |> Seq.takeWhile (fun _ -> queue.[0] |> Seq.isEmpty |> not)
        |> Seq.length
        |> fun _ -> total.[0]
|> printfn "%d"

// https://atcoder.jp/contests/joi2012yo/submissions/17223187
