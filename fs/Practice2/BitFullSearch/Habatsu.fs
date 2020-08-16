// https://atcoder.jp/contests/abc002/tasks/abc002_4

stdin.ReadLine()
|> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (n, m) ->
  (
    n,
    [1..m] |> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map (int >> (+) -1) |> fun x -> (x.[0], x.[1]))
  )
|> fun (n, ms) ->
  (
    n,
    ms @ (ms |> List.map (fun (a, b) -> b, a)) |> Set.ofList
  )
|> fun (n, ms) ->
  [((1 <<< n) - 1)..(-1)..0]
  |> List.map (
    fun c ->
      [0..(n - 1)]
      |> List.filter (fun i -> c >>> i &&& 1 = 1)
  )
  |> List.sortBy List.length
  |> List.rev
  |> List.find (
    fun members ->
      members
      |> List.forall (
        fun m1 ->
          members
          |> List.filter (fun m2 -> m1 <> m2)
          |> List.map (fun m2 -> m1, m2)
          |> List.forall ms.Contains
      )
  )
|> Seq.length
|> printfn "%d"

// https://atcoder.jp/contests/abc002/submissions/13347170
