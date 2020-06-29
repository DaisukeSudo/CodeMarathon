// https://atcoder.jp/contests/abs/tasks/abc085_c

let fx = fun n y i -> y - n - 9 * i
let fj = fun x -> x / 4
let fk = fun n i j -> n - i - j

let fc4 = fun j k -> j >= 0 && k >= 0
let fc3 = fun n i j -> fc4 j (fk n i j)
let fc2 = fun n i x -> if x % 4 = 0 then (fc3 n i (fj x)) else false
let fc1 = fun n y i -> fc2 n i (fx n  y  i)

let fo4 = fun i j k -> sprintf "%d %d %d" i j k
let fo3 = fun n i j -> fo4 i j (fk n i j)
let fo2 = fun n i x -> fo3 n i (fj x)
let fo1 = fun n y i -> match i with | Some i -> fo2 n i (fx n y i) | None -> "-1 -1 -1"

let fc = fun n y -> [0..(min 2000 (y / 10))] |> List.tryFind (fc1 n y)
let fo = fun n y -> fo1 n y (fc n y)

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> fo x.[0] (x.[1] / 1000)
|> printfn "%s"

// https://atcoder.jp/contests/abs/submissions/9410338

// ----------

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1] / 1000)
|> fun (n, y) ->
  [0..(min 2000 (y / 10))]
  |> List.tryFind (
    fun i ->
      (y - n - 9 * i)
      |> fun x ->
        if x % 4 = 0
        then
          (x / 4)
          |> fun j ->
            (n - i - j)
            |> fun k -> j >= 0 && k >= 0
        else
          false
  )
  |> (
    fun i ->
      match i with
      | Some i ->
        (y - n - 9 * i)
        |> fun x ->
          (x / 4)
          |> fun j ->
            (n - i - j)
            |> fun k -> sprintf "%d %d %d" i j k
      | None -> "-1 -1 -1"
  )
  |> printfn "%s"

// ----------

stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1] / 1000) |> fun (n, y) -> [0..(min 2000 (y / 10))] |> List.tryFind (fun i -> (y - n - 9 * i) |> fun x -> if x % 4 = 0 then (x / 4) |> fun j -> (n - i - j) |> fun k -> j >= 0 && k >= 0 else false) |> (fun i -> match i with | Some i -> ((y - n - 9 * i) |> fun x -> (x / 4) |> fun j -> (n - i - j) |> fun k -> sprintf "%d %d %d" i j k) | None -> "-1 -1 -1") |> printfn "%s"

// https://atcoder.jp/contests/abs/submissions/9414832

// ----------
