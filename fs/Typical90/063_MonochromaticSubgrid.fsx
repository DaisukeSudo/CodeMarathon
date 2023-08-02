// https://atcoder.jp/contests/typical90/tasks/typical90_bk

let h, w = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let p = [| 1 .. h |] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map int)

[1 .. (1 <<< h) - 1] |> List.map (fun i ->
  let cntW =
    [0 .. w - 1]
    |> List.fold (fun r j ->
      [0 .. h - 1]
      |> List.filter (fun k -> (i &&& (1 <<< k)) <> 0)
      |> List.map (fun k -> p.[k].[j])
      |> List.distinct
      |> fun x ->
        match x with
        | x :: [] -> x :: r
        | _ -> r
    ) []
    |> Seq.fold (fun m x ->
      m |> Map.add x ((m |> Map.tryFind x |> Option.defaultValue 0) + 1)
    ) Map.empty
    |> Map.toList
    |> List.map (fun (_, v) -> v)
    |> List.fold max 0

  let cntH =
    [0 .. h - 1]
    |> List.filter (fun j -> (i &&& (1 <<< j) <> 0))
    |> List.length

  cntH * cntW
)
|> List.max
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/43988592
