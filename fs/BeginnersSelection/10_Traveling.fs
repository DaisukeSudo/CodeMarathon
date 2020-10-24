// https://atcoder.jp/contests/abs/tasks/arc089_a

// stdin.ReadLine()
// |> int
// |> fun x -> [1..x]
// |> List.map (fun _ -> stdin.ReadLine())
// |> List.map (
//   fun x ->
//     x.Split(' ')
//     |> Array.map int
//     |> Array.toList
// )
// |> List.map (fun x -> (x.[0], x.[1], x.[2]))
// |> List.fold (
//   fun acc (t2, x2, y2) ->
//     match acc with
//     | Some (t1, x1, y1) -> (
//       if ((t2 &&& 1) = ((x2 + y2) &&& 1))
//         && ((t2 - t1) >= (abs (x2 - x1) + abs (y2 - y1)))
//       then Some (t2, x2, y2)
//       else None
//     )
//     | None -> None
// ) (Some (0, 0, 0))
// |> fun x -> (
//   match x with
//   | Some _ -> "Yes"
//   | None -> "No"
// )
// |> printfn "%s"

// ----------

// stdin.ReadLine() |> int |> fun x -> [1..x] |> List.map (fun _ -> stdin.ReadLine()) |> List.map (fun x -> x.Split(' ') |> Array.map int |> Array.toList) |> List.map (fun x -> (x.[0], x.[1], x.[2])) |> List.fold (fun acc (t2, x2, y2) -> match acc with | Some (t1, x1, y1) -> (if ((t2 &&& 1) = ((x2 + y2) &&& 1)) && ((t2 - t1) >= (abs (x2 - x1) + abs (y2 - y1))) then Some (t2, x2, y2) else None) | None -> None) (Some (0, 0, 0)) |> fun x -> (match x with | Some _ -> "Yes" | None -> "No") |> printfn "%s"

// https://atcoder.jp/contests/abs/submissions/9414933

// ----------

stdin.ReadLine()
|> int
|> fun x -> [1..x]
|> List.map (
  fun _ ->
    stdin.ReadLine()
    |> fun x -> x.Split(' ')
    |> Array.map int
    |> fun x -> (x.[0], x.[1], x.[2])
)
|> List.fold (
  fun acc (t2, x2, y2) ->
    match acc with
    | Some (t1, x1, y1) ->
      (
        if ((t2 &&& 1) = ((x2 + y2) &&& 1))
          && ((t2 - t1) >= (abs (x2 - x1) + abs (y2 - y1)))
        then Some (t2, x2, y2)
        else None
      )
    | None -> None
) (Some (0, 0, 0))
|> fun x ->
  (
    match x with
    | Some _ -> "Yes"
    | None -> "No"
  )
|> printfn "%s"
