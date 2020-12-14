// https://atcoder.jp/contests/joi2013yo/tasks/joi2013yo_d

stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (d, n) ->
  (
    Array.init d (fun _ -> stdin.ReadLine() |> int),
    Array.init n (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> ((x.[0], x.[1], x.[2]), x.[2]))
    |> Array.unzip
  )
|> fun (ts : int [], (abcs : (int * int * int) [], cs : int [])) ->
  abcs
  |> Array.map (fun (a, b, _) -> if a <= ts.[0] && ts.[0] <= b then 0 else -1)
  |> fun memo0 ->
    ts.[1..]
    |> Array.fold (
      fun memo t ->
        abcs
        |> Array.map (
          fun (a, b, c) ->
            if a <= t && t <= b
            then (
              memo
              |> Array.mapi (fun j v -> if v >= 0 then (abs (cs.[j] - c) + v) else -1)
              |> Array.max
            )
            else -1
        )
    ) memo0
  |> Array.max
|> printfn "%d"


// input:
// 3 4
// 31
// 27
// 35
// 20 25 30
// 23 29 90
// 21 35 60
// 28 33 40

// t: 31, memo: [|-1; -1;  0;  0|]
// t: 27, memo: [|-1; 50; 20; -1|]
// t: 35, memo: [|-1; -1; 80; -1|]

// output:
// 80


// https://atcoder.jp/contests/joi2013yo/submissions/18783378
