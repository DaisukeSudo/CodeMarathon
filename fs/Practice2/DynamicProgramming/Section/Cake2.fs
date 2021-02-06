// https://atcoder.jp/contests/joi2015ho/tasks/joi2015ho_b

stdin.ReadLine ()
|> int
|> fun n ->
  (
    (Array.init n (fun _ -> stdin.ReadLine () |> int64) |> fun arr -> [ arr; arr ] |> Array.concat),
    (Array.init n (fun _ -> Array.create n 0L)),
    [| fun _ _ -> 0L |]
  )
  |> fun (cakes, memo, fnc) ->
    (
      (* "start" and "count" mean the range of this turn *)
      fun start count ->
        match count with
        | 0 -> cakes.[start]
        | x when x < 0 -> 0L
        (* If the memo has a value, return it *)
        | _ when memo.[start % n].[count] <> 0L -> memo.[start % n].[count]
        | _ ->
          [
            (start,         [(start + 1, 2); (start + count, 1)]); (* JOI takes a value from head *)
            (start + count, [(start, 1); (start + count - 1, 0)]); (* JOI takes a value from last *)
          ]
          |> List.map (
            fun (joi, ioi) ->
              ioi
              (* IOI takes a larger value *)
              |> List.maxBy (fun (i, _) -> cakes.[i])
              (* Getting a value from the rest of the range *)
              |> fun (_, offset) -> (fnc.[0] (start + offset) (count - 2))
              (* Adding the value acquired this turn *)
              |> (+) cakes.[joi]
          )
          (* Adopting the maximum value of this range *)
          |> List.max
          (* Setting the value in the memo and return it *)
          |> fun v -> (memo.[start % n].[count] <- v) |> fun () -> v
    )
    |> fun fn ->
      (fnc.[0] <- fn)
      |> fun () ->
        [0..(n - 1)]
        |> List.map (fun i -> fn i (n - 1))
        |> List.max
|> printfn "%d"

// https://atcoder.jp/contests/joi2015ho/submissions/20004942
// https://atcoder.jp/contests/joi2015ho/submissions/20005189
