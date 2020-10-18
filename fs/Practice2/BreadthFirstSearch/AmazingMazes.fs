// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1166

Seq.initInfinite (fun _ -> stdin.ReadLine())
|> Seq.takeWhile ((<>) "0 0")
|> Seq.fold (
  fun (hs: (int * int) list, wss: int [] [] list, n: int, i: int) (s: string) ->
    (s.Trim().Split(' ') |> Array.map int)
    |> fun line ->
      if n - i = 0
      then (
        (line.[0], line.[1])
        |> fun (w, h) ->
          (h * 2 - 1)
          |> fun n -> ((w, h)::hs, (Array.create n [||])::wss, n, 0)
      )
      else (
        (wss.[0].[i] <- line)
        |> fun () -> (hs, wss, n, i + 1)
      )
) ([], [], 0, 0)
|> fun (hs, wss, _, _) -> (List.zip hs wss)
|> List.rev
|> List.map (
  fun ((w, h), wss: int [] []) ->
    (
      h - 1,
      w - 1,
      Array.init h (fun _ -> Array.create w 0)
    )
    |> fun (gi, gj, field: int [] []) ->
      (field.[0].[0] <- 1)
      |> fun () -> [| [ (0, 0) ] |]
      |> fun (queue: (int * int) list array) ->
        Seq.initInfinite (
          fun _ ->
            match queue.[0] with
            | [] -> ()
            | (i, j)::ts ->
              (field.[i].[j])
              |> fun step ->
                [
                  ((i - 1, j    ), (-1,  0));
                  ((i    , j + 1), ( 0,  0));
                  ((i + 1, j    ), ( 1,  0));
                  ((i    , j - 1), ( 0, -1));
                ]
                |> List.filter (fun ((i2, j2), _) -> 0 <= i2 && i2 <= gi && 0 <= j2 && j2 <= gj)
                |> List.filter (fun ((i2, j2), _) -> field.[i2].[j2] = 0)
                |> List.filter (fun (_, (m, n)) -> wss.[i * 2 + m].[j + n] = 0)
                |> List.map (fun ((i2, j2), _) -> (field.[i2].[j2] <- step + 1) |> fun () -> (i2, j2))
                |> fun ts2 -> queue.[0] <- ts @ ts2
        )
        |> Seq.takeWhile (fun _ -> field.[gi].[gj] = 0 && (queue.[0] |> Seq.isEmpty |> not))
        |> Seq.length
        |> fun _ -> field.[gi].[gj]
        |> string
)
|> String.concat "\n"
|> printfn "%s"

// (0, 0) -> (0, 1) : 0, 0
// (0, 0) -> (1, 0) : 1, 0

// (1, 1) -> (0, 1) : 1, 1
// (1, 1) -> (1, 2) : 2, 1
// (1, 1) -> (2, 1) : 3, 1
// (1, 1) -> (1, 0) : 2, 0

// (2, 2) -> (1, 2) : 3, 2
// (2, 2) -> (2, 3) : 4, 2
// (2, 2) -> (3, 2) : 5, 2
// (2, 2) -> (2, 1) : 4, 1
