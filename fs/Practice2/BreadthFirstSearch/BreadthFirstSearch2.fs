// https://atcoder.jp/contests/abc007/tasks/abc007_3

(
  (stdin.ReadLine() |> fun x -> x.Split() |> Array.map int |> fun x -> (x.[0], x.[1])),
  (stdin.ReadLine() |> fun x -> x.Split() |> Array.map (int >> (+) -1) |> fun x -> (x.[0], x.[1])),
  (stdin.ReadLine() |> fun x -> x.Split() |> Array.map (int >> (+) -1) |> fun x -> (x.[0], x.[1]))
)
|> fun ((r, c), (sy, sx), (gy, gx)) ->
  (
    fun _ ->
      stdin.ReadLine()
      |> Seq.map (string >> (fun x -> if x = "." then -1 else -9))
      |> Seq.toArray
  )
  |> Array.init r
  |> fun (field: int array array) ->
    (field.[sy].[sx] <- 0)
    |> fun _ -> [| [ (sy, sx) ] |]
    |> fun ((queue: (int * int) list array)) ->
      Seq.initInfinite (
        fun _ ->
          match queue.[0] with
          | [] -> ()
          | (y, x)::ts ->
            field.[y].[x]
            |> fun step ->
              [
                (y - 1, x    );
                (y    , x + 1);
                (y + 1, x    );
                (y    , x - 1);
              ]
              |> List.filter (fun (y2, x2) -> field.[y2].[x2] = -1)
              |> List.map (fun (y2, x2) -> (field.[y2].[x2] <- step + 1) |> fun _ -> (y2, x2))
              |> fun ts2 -> queue.[0] <- ts @ ts2
      )
      |> Seq.takeWhile (fun _ -> field.[gy].[gx] = -1 && (queue.[0] |> Seq.isEmpty |> not))
      |> Seq.length
      |> fun _ -> field.[gy].[gx]
|> printfn "%d"

// https://atcoder.jp/contests/abc007/submissions/17011254
