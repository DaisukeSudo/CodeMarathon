// https://atcoder.jp/contests/sumitrust2019/tasks/sumitb2019_d

// 4
// 0224
// 3

// 19
// 3141592653589793238
// 329

// ----

let fn1: string -> int =
  Seq.distinct >> Seq.length

let fn2: string -> int =
  fun s ->
    [0..(s.Length - 1)]
    |> Seq.fold (fun m i ->
      match Map.tryFind s.[i] m with
      | Some _ -> m
      | _ -> m.Add (s.[i], s.[(i + 1)..] |> fn1)
    ) Map.empty
    |> Seq.sumBy (fun x -> x.Value)

let fn3: string -> int =
  fun s ->
    [0..(s.Length - 1)]
    |> Seq.fold (fun m i ->
      match Map.tryFind s.[i] m with
      | Some _ -> m
      | _ -> m.Add (s.[i], s.[(i + 1)..] |> fn2)
    ) Map.empty
    |> Seq.sumBy (fun x -> x.Value)

stdin.ReadLine()
|> ignore
|> fun () -> stdin.ReadLine()
|> fn3
|> printfn "%d"

// https://atcoder.jp/contests/sumitrust2019/submissions/11458660
// https://atcoder.jp/contests/sumitrust2019/submissions/11459204

// ----

stdin.ReadLine()
|> ignore
|> fun () -> stdin.ReadLine()
|> fun s ->
  [0..(s.Length - 1)]
  |> Seq.fold (fun m i ->
    match Map.tryFind s.[i] m with
    | Some _ -> m
    | _ ->
      m.Add (
        s.[i],
        s.[(i + 1)..] |> (
          fun s ->
            [0..(s.Length - 1)]
            |> Seq.fold (fun m i ->
              match Map.tryFind s.[i] m with
              | Some _ -> m
              | _ ->
                m.Add (
                  s.[i],
                  s.[(i + 1)..]
                  |> Seq.distinct
                  |> Seq.length
                )
            ) Map.empty
            |> Seq.sumBy (fun x -> x.Value)
        )
      )
  ) Map.empty
  |> Seq.sumBy (fun x -> x.Value)
|> printfn "%d"

// https://atcoder.jp/contests/sumitrust2019/submissions/11458923
