// https://onlinejudge.u-aizu.ac.jp/problems/ALDS1_13_A

stdin.ReadLine()
|> int
|> fun n -> [1..n]
|> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
|> fun rcs ->
  [0..7]
  |> List.filter (fun x -> rcs |> List.map fst |> List.contains x |> not)
  |> List.fold (
    fun rcss x ->
      rcss
      |> List.collect (
        fun rcs ->
          rcs
          |> List.collect (
            fun (r, c) ->
              ([0..7] |> List.map (fun x -> (r, x))) @
              ([0..7] |> List.map (fun x -> (x, c))) @
              ([0..7] |> List.map (fun x -> (x + r - c, x))) @
              ([0..7] |> List.map (fun x -> (x, - x + r + c)))
          )
          |> fun occupiedArea ->
            [0..7]
            |> List.map (fun y -> (x, y))
            |> List.filter (fun rc -> occupiedArea |> List.contains rc |> not)
            |> List.map (fun rc -> rc :: rcs)
      )
  ) [rcs]
|> List.head
|> fun queens ->
  [0..7]
  |> List.map (
    fun x ->
      [0..7]
      |> List.map (fun y -> if queens |> List.contains (x, y) then "Q" else ".")
      |> String.concat ""
  )
  |> String.concat "\n"
|> printfn "%s"

// https://atcoder.jp/contests/abs/custom_test

// ----

// 2
// 2 2
// 5 3

// ......Q.
// Q.......
// ..Q.....
// .......Q
// .....Q..
// ...Q....
// .Q......
// ....Q...

// ----

let occupiedAreaW =
  fun rcs ->
    rcs
    |> List.collect (
      fun (r, c) ->
        ([0..7] |> List.map (fun x -> (r, x))) @
        ([0..7] |> List.map (fun x -> (x, c))) @
        ([0..7] |> List.map (fun x -> (x + r - c, x))) @
        ([0..7] |> List.map (fun x -> (x, - x + r + c)))
    )
    |> fun occupiedArea ->
      [0..7]
      |> List.map (
        fun x ->
          [0..7]
          |> List.map (fun y -> if occupiedArea |> List.contains (x, y) then "X" else "-")
          |> String.concat " "
      )
      |> String.concat "\n"
    |> printfn "%s"


let queensW =
  fun queens ->
    [0..7]
    |> List.map (
      fun x ->
        [0..7]
        |> List.map (fun y -> if queens |> List.contains (x, y) then "Q" else ".")
        |> String.concat " "
    )
    |> String.concat "\n"
    |> printfn "%s"
