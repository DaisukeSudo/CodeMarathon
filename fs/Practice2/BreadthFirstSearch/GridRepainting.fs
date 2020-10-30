// https://atcoder.jp/contests/abc088/tasks/abc088_d

(-1, -9)
|> fun (white, black) ->
  stdin.ReadLine()
  |> fun x -> x.Split()
  |> Array.map int
  |> fun x -> (x.[0], x.[1])
  |> fun (h, w) ->
    Array.init h (fun _ -> stdin.ReadLine() |> Seq.map (fun x -> if x = '.' then white else black) |> Seq.toArray)
    |> fun (grid: int [] []) ->
      (
        Array.concat [ [| Array.create w black |]; grid; [| Array.create w black |] ]
        |> Array.map (fun row -> Array.concat [ [| black |]; row; [| black |] ]),
        grid
        |> Array.sumBy (Array.filter ((=) white) >> Array.length)
      )
    |> fun (grid: int [] [], wcount: int) ->
      (grid.[1].[1] <- 1)
      |> fun () -> [| [ (1, 1) ] |]
      |> fun (queue: (int * int) list array) ->
        Seq.initInfinite (
          fun _ ->
            match queue.[0] with
            | [] -> ()
            | (i, j)::ts ->
              (grid.[i].[j])
              |> fun step ->
                [
                  (i - 1, j    );
                  (i    , j + 1);
                  (i + 1, j    );
                  (i    , j - 1);
                ]
                |> List.filter (fun (i2, j2) -> grid.[i2].[j2] = white)
                |> List.map (fun (i2, j2) -> (grid.[i2].[j2] <- step + 1) |> fun () -> (i2, j2))
                |> fun ts2 -> queue.[0] <- ts @ ts2
        )
        |> Seq.takeWhile (fun _ -> grid.[h].[w] = white && (queue.[0] |> Seq.isEmpty |> not))
        |> Seq.length
        |> fun _ -> grid.[h].[w]
        |> fun step -> if step = white then step else wcount - step
|> printfn "%d"

// https://atcoder.jp/contests/abc088/submissions/17528933
