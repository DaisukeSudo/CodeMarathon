// https://atcoder.jp/contests/abc074/tasks/arc083_b

stdin.ReadLine() |> int
|> fun n ->
  (
    fun () -> Array.init n (fun _ -> Array.create n 0)
  )
  |> fun createArr -> (createArr (), createArr (), [| true |])
  |> fun (gI, gO, isContinue) ->
    seq { 0 .. n - 1 }
    |> Seq.iter (fun i ->
      stdin.ReadLine().Split()
      |> Array.mapi (fun i x -> i, int x)
      |> Array.iter (fun (j, x) -> ((gI.[i].[j] <- x), (gO.[i].[j] <- x)) |> ignore)
    )
    |> fun _ ->
      seq {
        for k in 0 .. n - 1 do
        for i in 0 .. n - 1 do
        for j in i + 1 .. n - 1 do
        if i <> k && j <> k then yield (i, j, k)
      }
      |> Seq.takeWhile (fun _ -> isContinue.[0])
      |> Seq.iter (fun (i, j, k) ->
        match (gI.[i].[j] - gI.[i].[k] - gI.[k].[j]) with
        | x when x = 0 -> gO.[i].[j] <- 0
        | x when x > 0 -> isContinue.[0] <- false
        | _ -> ()
      )
      |> fun _ ->
        if isContinue.[0]
        then (
          seq {
            for i in 0 .. n - 1 do
            for j in i + 1 .. n - 1 do
            yield int64 gO.[i].[j]
          }
          |> Seq.sum
        )
        else -1L
  |> printfn "%d"

// https://atcoder.jp/contests/abc074/submissions/23391450
