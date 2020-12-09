// https://atcoder.jp/contests/joi2012yo/tasks/joi2012yo_d

(fun () -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
|> fun readLineAsTuple2 ->
  readLineAsTuple2 ()
  |> fun (n, k) ->
    (Array.create n -1)
    |> fun (decided : int []) ->
      [1..k]
      |> List.map (ignore >> readLineAsTuple2)
      |> List.iter (fun (a, b) -> decided.[a - 1] <- b - 1)
      |> fun _ -> (fun i x -> decided.[i] = -1 || decided.[i] = x)
    |> fun (canSelect : int -> int -> bool) ->
      [| 0..2 |]
      |> Array.map (fun x -> if canSelect 0 x then 1 else 0)
      |> fun memo0 ->
        [| 0..26 |]
        |> Array.map (
          fun x ->
            (x / 9, x / 3 % 3, x % 3)
            |> fun (t1, t2, t3) ->
              if canSelect 1 t1 && t2 = t3 then memo0.[t3] else 0
        )
        |> fun memo1 ->
          [2..(n - 1)]
          |> List.fold (
            fun memo i ->
              [| 0..26 |]
              |> Array.map (
                fun x ->
                  (x / 9, x / 3 % 3, x % 3)
                  |> fun (t1, t2, t3) ->
                    if canSelect i t1 && (t1 <> t2 || t1 <> t3 || t2 <> t3)
                    then (
                      (t2 * 9 + t3 * 3)
                      |> fun j -> [j..(j + 2)]
                      |> List.sumBy (fun j -> memo.[j])
                      |> fun a -> a % 10000
                    )
                    else 0
              )
          ) memo1
        |> Array.sum
        |> fun a -> a % 10000
|> printfn "%d"

// https://atcoder.jp/contests/joi2012yo/submissions/18656809
