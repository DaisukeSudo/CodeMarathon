// https://atcoder.jp/contests/tenka1-2018-beginner/tasks/tenka1_2018_d

let judge =
  fun n ->
    seq {1..448}
    |> Seq.map (fun k -> k * (k - 1) / 2)
    |> Seq.tryFindIndex ((=) n)

// seq {100000..(-1)..10000} |> Seq.filter (judge >> ((<>) None)) |> Seq.head
// 99681

let sample =
  fun n ->
    (Array.create 448 [])
    |> fun sgs ->
      [1..n]
      |> List.fold (fun (i, j) x ->
        [
          sgs.[i]     <- x :: sgs.[i]
          sgs.[j % i] <- x :: sgs.[j % i]
        ]
        |> fun _ ->
          if j < i then (i, j + 1) else (i + 1, 1)
      ) (1, 1)
      |> fun (k, _) ->
        sgs.[0..k - 1]
        |> Array.map (
          List.map string >> String.concat " " >> sprintf "%d %s" (k - 1)
        )
        |> String.concat "\n"
        |> sprintf "%d\n%s" k

stdin.ReadLine() |> int
|> fun n -> 
  match judge n with
  | Some(_) -> "Yes\n" + sample n
  | None -> "No" 
|> printfn "%s"

// https://atcoder.jp/contests/tenka1-2018-beginner/submissions/30425615
