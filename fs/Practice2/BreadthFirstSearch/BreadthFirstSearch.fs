// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_11_C

stdin.ReadLine()
|> int
|> fun n ->
  (
    fun _ ->
      stdin.ReadLine()
      |> fun x -> x.Split()
      |> Array.map int
      |> fun x -> (x.[0], x.[2..] |> Array.toList)
  )
  |> List.init n 
|> fun nodes ->
  nodes
  |> Map.ofList
  |> fun graph ->
    nodes
    |> List.map(
      fun (goal, _) ->
        (
          [| [ (1, []) ] |],
          [| -1 |]
        )
        |> fun ((queue: (int * int list) list array), (step: int array)) ->
          Seq.initInfinite (
            fun _ ->
              match queue.[0] with
              | [] -> ()
              | (pos, route)::ts ->
                if pos = goal
                then (
                  (queue.[0] <- [])
                  |> fun _ -> step.[0] <- route |> List.length
                )
                else (
                  graph
                  |> Map.find pos
                  |> List.filter (fun x -> route |> List.contains x |> not)
                  |> List.map (fun x -> (x, pos::route))
                  |> fun ts2 -> queue.[0] <- ts @ ts2
                )
          )
          |> Seq.takeWhile (fun _ -> queue.[0] |> Seq.isEmpty |> not)
          |> Seq.length
          |> fun _ -> [goal; step.[0]] |> List.map string |> String.concat " "
    )
|> String.concat "\n"
|> printfn "%s"
