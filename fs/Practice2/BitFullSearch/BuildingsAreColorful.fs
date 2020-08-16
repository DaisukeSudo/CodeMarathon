// https://atcoder.jp/contests/s8pc-4/tasks/s8pc_4_b

(
  stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]),
  stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int64
)
|> fun ((n, k), buildings) ->
  [0..((1 <<< (n - 1)) - 1)]
  |> List.map (
    fun candidates ->
      [0..(n - 2)]
      |> List.fold (
        fun (prev, num, cost) i ->
          buildings.[i + 1]
          |> fun cur ->
            if prev < cur
            then (cur, num + 1, cost)
            else (
              if candidates >>> i &&& 1 = 1
              then (prev + 1L, num + 1, cost + (prev - cur + 1L))
              else (prev, num, cost)
            )
      ) (buildings.[0], 1, 0L)
  )
  |> List.filter (fun (_, num, _) -> num >= k)
  |> List.minBy (fun (_, _, cost) -> cost)
|> printfn "%d"

// https://atcoder.jp/contests/s8pc-4/submissions/14083953
