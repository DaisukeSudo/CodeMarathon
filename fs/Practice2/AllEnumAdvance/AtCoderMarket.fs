// https://atcoder.jp/contests/s8pc-6/tasks/s8pc_6_b

//                  [I] [O]
//           1 2 3 4 5 6 7 8 9 A
// C1: 5 7 $         S > G
// C2: 2 6 $   * < < S
//             * > > > > G
// C3: 8 A $         S > > > > *
//                       G < < * 

// Case    | Sample1 | Sample2 | Sample3    |
// Correct |      18 |     334 | 8494550716 |
// Mean    |      19 |     352 | 8605396813 |
// Median  |      18 |     334 | 8494550716 |

let mean =
  fun list ->
    list |> Seq.averageBy float |> int64

let median =
  fun list ->
    (
      list |> Seq.sort |> Seq.toArray,
      list |> Seq.length
    )
    |> fun (arr, len) ->
      if len % 2 = 0
      then ([arr.[len / 2]; arr.[len / 2 + 1]] |> List.distinct)
      else [arr.[len / 2]]

stdin.ReadLine()
|> int
|> fun n -> [1..n]
|> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int64 |> fun x -> (x.[0], x.[1]))
|> fun list ->
  list
  |> List.unzip
  |> fun (l1, l2) ->
    (
      l1 |> median,
      l2 |> median
    )
  |> fun (ss, gs) ->
    ss
    |> List.fold (
      fun acc s ->
        gs
        |> List.fold (
          fun acc g ->
            list
            |> List.sumBy (
              fun (a, b) ->
                [
                  (s - a) |> abs;
                  (a - b) |> abs;
                  (b - g) |> abs;
                ]
                |> List.sum
            )
            |> min acc
        ) acc
        |> min acc
    ) System.Int64.MaxValue
|> printfn "%d"

// https://atcoder.jp/contests/s8pc-6/submissions/11949457
