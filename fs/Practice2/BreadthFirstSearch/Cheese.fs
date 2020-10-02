// https://atcoder.jp/contests/joi2011yo/tasks/joi2011yo_e

stdin.ReadLine()
|> fun x -> x.Split()
|> Array.map int
|> fun x -> (x.[0], x.[1], x.[2])
|> fun (h, w, n) ->
  Array.init h (fun _ -> stdin.ReadLine() |> Seq.map string |> Seq.toArray)
  |> fun (townMap: string [] []) ->
    Array.concat [ [| Array.create w "X" |]; townMap; [| Array.create w "X" |] ]
    |> Array.map (fun row -> Array.concat [ [| "X" |]; row; [| "X" |] ])
  |> fun (townMap: string [] []) ->
    townMap
    |> Array.mapi (
      fun i row ->
        row
        |> Array.tryFindIndex (fun x -> x = "S")
        |> Option.map (fun j -> (i, j))
    )
    |> Array.find (fun x -> x.IsSome)
    |> Option.get
    |> fun (si, sj) ->
      [1..n]
      |> List.map string
      |> List.fold (
        fun (si, sj, total) goal ->
          Array.init (h + 2) (fun _ -> Array.create (w + 2) -1)
          |> fun (field: int [] []) ->
            (field.[si].[sj] <- 0)
            |> fun _ ->
              (
                [| [ (si, sj) ] |],
                [| None |]
              )
            |> fun (queue: (int * int) list array, npos: (int * int) option array) ->
              Seq.initInfinite (
                fun _ ->
                  match queue.[0] with
                  | [] -> ()
                  | (i, j)::ts ->
                    if townMap.[i].[j] = goal
                    then
                      npos.[0] <- Some (i, j)
                    else
                      field.[i].[j]
                      |> fun step ->
                        [
                          (i - 1, j    );
                          (i    , j + 1);
                          (i + 1, j    );
                          (i    , j - 1);
                        ]
                        |> List.filter (fun (i2, j2) -> field.[i2].[j2] = -1 && townMap.[i2].[j2] <> "X")
                        |> List.map (fun (i2, j2) -> (field.[i2].[j2] <- step + 1) |> fun _ -> (i2, j2))
                        |> fun ts2 -> queue.[0] <- ts @ ts2
              )
              |> Seq.takeWhile (fun _ -> (npos.[0] |> Option.isNone) && (queue.[0] |> Seq.isEmpty |> not))
              |> Seq.length
              |> fun _ -> npos.[0] |> Option.get
              |> fun (gi, gj) -> (gi, gj, total + field.[gi].[gj])
      ) (si, sj, 0)
      |> fun (_, _, total) -> total
|> printfn "%A"

// https://atcoder.jp/contests/joi2011yo/submissions/17142355
