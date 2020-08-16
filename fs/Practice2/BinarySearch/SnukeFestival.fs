// https://atcoder.jp/contests/abc077/tasks/arc084_a

// (
//   (stdin.ReadLine() |> ignore),
//   (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.sort),
//   (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.sort),
//   (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.sort)
// )
// |> fun (_, arrA, arrB, arrC) ->
//     arrA
//     |> Array.sumBy (
//       fun av ->
//         arrB
//         |> Array.filter (fun bv -> av < bv)
//         |> Array.sumBy (
//           fun bv ->
//             arrC
//             |> Array.filter (fun cv -> bv < cv)
//             |> Array.length
//         )
//     )
// |> printfn "%d"

(
  (stdin.ReadLine() |> int),
  (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.sort),
  (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.sort),
  (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.sort)
)
|> fun (n, arrA, arrB, arrC) ->
  (
    fun (arr: int array) ->
      (
        (arr |> Array.length |> double, 2.) |> System.Math.Log |> ceil |> int,
        arr |> Array.max
      )
      |> fun (limI, maxV) ->
        fun v ->
          if v >= maxV
          then n
          else (
            [1..limI]
            |> List.fold (
              fun (li, ri) _ ->
                if li = ri then (li, ri) else (
                  li + (ri - li) / 2
                  |> fun mi ->
                    match compare v arr.[mi] with
                    | -1 -> (li, mi)
                    | _ -> (mi + 1, ri)
                )
            ) (0, arr.Length - 1)
            |> fun (_, i) -> i
          )
  )
  |> fun search ->
    (
      arrB
      |> Array.map (search arrC >> (-) n >> int64)
      |> Array.rev
      |> Array.scan (+) 0L
      |> Array.rev
    )
    |> fun arrBSize ->
      arrA
      |> Array.sumBy (search arrB >> (fun i -> arrBSize.[i]))
|> printfn "%d"

// https://atcoder.jp/contests/abc077/submissions/15314558

// ----

// let b1 = arrB
// let b2 = b1 |> Array.map (search arrC)
// let b3 = b2 |> Array.map ((-) n)
// let b4 = b3 |> Array.rev
// let b5 = b4 |> Array.scan (+) 0
// let b6 = b5 |> Array.rev

// printfn "b1: %A" b1
// printfn "b2: %A" b2
// printfn "b3: %A" b3
// printfn "b4: %A" b4
// printfn "b5: %A" b5
// printfn "b6: %A" b6

// let a1 = arrA
// let a2 = a1 |> Array.map (search arrB)
// let a3 = a2 |> Array.map ((fun i -> arrBSize.[i]) >> int64)

// printfn "a1: %A" a1
// printfn "a2: %A" a2
// printfn "a3: %A" a3
