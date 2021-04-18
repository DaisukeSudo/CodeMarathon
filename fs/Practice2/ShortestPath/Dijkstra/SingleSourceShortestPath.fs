// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_1_A

(fun _x -> stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1], x.[2]))
|> fun readAsNum3 ->
  readAsNum3 0
  |> fun (v, e, r) ->
    (
      Array.init e readAsNum3,
      Array.create v [],
      Array.create v System.Int32.MaxValue,
      [| Set [] |]
    )
    |> fun (stds, dict, memo, touched) ->
      (
        (stds |> Array.iter (fun (s, t, d) -> dict.[s] <- (t, d)::dict.[s])),
        (memo.[r] <- 0)
      )
      |> fun _ -> [1..v]
      |> List.fold (
        fun os _ ->
          match os with
          | None -> None
          | Some s ->
            dict.[s]
            |> List.filter (fun (t, d) -> memo.[t] > memo.[s] + d)
            |> List.map (fun (t, d) -> (memo.[t] <- memo.[s] + d) |> fun _ -> t)
            |> Set.ofList
            |> ((+) touched.[0])
            |> fun ts ->
              if ts.IsEmpty
              then None
              else (
                ts
                |> Seq.toList
                |> Seq.minBy (fun x -> memo.[x])
                |> fun next ->
                  (touched.[0] <- ts.Remove next)
                  |> fun _ -> Some next
              )
      ) (Some r)
      |> fun _ ->
        memo
        |> Array.map (fun x -> if x = System.Int32.MaxValue then "INF" else (string x))
        |> String.concat "\n"
|> printfn "%s"


// ipput:
// 4 5 0
// 0 1 1
// 0 2 4
// 1 2 2
// 2 3 1
// 1 3 5

// output:
// 0
// 1
// 3
// 4

// steps:
// s: 0
//   dict.[s]: [(2, 4); (1, 1)]
//   memo: [|0; 1; 4; 2147483647|]
//   ts: [1; 2]
// s: 1
//   dict.[s]: [(3, 5); (2, 2)]
//   memo: [|0; 1; 3; 6|]
//   ts: [2; 3]
// s: 2
//   dict.[s]: [(3, 1)]
//   memo: [|0; 1; 3; 4|]
//   ts: [3]
// s: 3
//    dict.[s]: []
//    memo: [|0; 1; 3; 4|]
//    ts: []
