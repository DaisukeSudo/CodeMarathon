// https://atcoder.jp/contests/joi2009yo/tasks/joi2009yo_d

(
  stdin.ReadLine() |> int,
  stdin.ReadLine() |> int
)
|> fun (m, n) ->
  Array.init n (fun _ -> stdin.ReadLine() |> fun x -> x.Split() |> Array.map (fun x -> x = "1"))
  |> fun (inputIces: bool array array) ->
    Array.concat [ [| Array.create m false |]; inputIces; [| Array.create m false |] ]
    |> Array.map (fun rows -> Array.concat [ [| false |]; rows; [| false |] ])
  |> fun (initialIces: bool array array) ->
    [1..n]
    |> List.collect (fun x1 -> [1..m] |> List.map (fun x2 -> (x1, x2)))
    |> List.filter (fun (i, j) -> initialIces.[i].[j])
    |> List.map (
      fun (i, j) ->
        (
          [| [((i, j), Set.empty)] |],
          [| 0 |]
        )
        |> fun ((stack: ((int * int) * Set<int * int>) list []), (ans: int [])) ->
          Seq.initInfinite (
            fun _ ->
              match stack.[0] with
              | [] -> ()
              | ((i, j), brokenIces)::ts ->
                [
                  (i - 1, j    );
                  (i    , j + 1);
                  (i + 1, j    );
                  (i    , j - 1);
                ]
                |> List.filter (
                  fun (i2, j2) ->
                    initialIces.[i2].[j2] && (brokenIces |> Set.contains (i2, j2) |> not)
                )
                |> fun nextSteps ->
                  match nextSteps with
                  | [] ->
                    (stack.[0] <- ts)
                    |> fun () -> (ans.[0] <- brokenIces |> Set.count |> (+) 1 |> max ans.[0])
                  | nextSteps ->
                    brokenIces |> Set.add (i, j)
                    |> fun brokenIces2 ->
                      nextSteps
                      |> List.map (fun (i2, j2) -> ((i2, j2), brokenIces2))
                      |> fun hs -> (stack.[0] <- hs @ ts)
          )
          |> Seq.takeWhile (fun _ -> stack.[0] |> List.isEmpty |> not)
          |> Seq.length
          |> fun _ -> ans.[0]
    )
    |> List.max
|> printfn "%d"

// https://atcoder.jp/contests/joi2009yo/submissions/16564267
