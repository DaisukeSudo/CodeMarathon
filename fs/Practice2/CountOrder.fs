// https://atcoder.jp/contests/abc150/tasks/abc150_c

(
  stdin.ReadLine() |> int,
  stdin.ReadLine(),
  stdin.ReadLine()
)
|> fun (n, ps, qs) ->
  [1..n]
  |> fun list ->
    list
    |> List.tail
    |> List.fold (
      fun acc i ->
        acc
        |> List.collect (
          fun cs ->
            list
            |> List.except cs
            |> List.map (fun x -> cs @ [x])
        )
    ) (list |> List.map (fun x -> [x]))
    |> List.mapi (
      fun i order ->
        (
          i,
          order
          |> List.map string
          |> List.reduce (fun a b -> a + " " + b)
        )
    )
    |> fun dict ->
      (
        (dict |> List.find (fun (_, v) -> v = ps) |> fun (i, _) -> i),
        (dict |> List.find (fun (_, v) -> v = qs) |> fun (i, _) -> i)
      )
|> fun (a, b) -> (a - b) |> abs
|> printfn "%d"

// [1; 2; 3]
// [
//   [1]
//   [2] 
//   [3]
// ]
// [
//   [1; 2]
//   [1; 3]
//   [2; 1]
//   [2; 3]
//   [3; 1]
//   [3; 2]
// ]
// [
//   [1; 2; 3]
//   [1; 3; 2]
//   [2; 1; 3]
//   [2; 3; 2]
//   [3; 1; 2]
//   [3; 2; 1]
// ]

// https://atcoder.jp/contests/abc150/submissions/14392165
