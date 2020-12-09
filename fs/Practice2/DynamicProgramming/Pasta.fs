// https://atcoder.jp/contests/joi2012yo/tasks/joi2012yo_d

(-1)
|> fun none ->
  (
    (stdin.ReadLine() |> int),
    (stdin.ReadLine() |> int)
  )
  |> fun (n, k) ->
    (Array.create n none)
    |> fun (decided : int []) ->
      [1..k]
      |> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
      |> List.iter (fun (a, b) -> decided.[a - 1] <- b - 1)
      |> fun _ -> (fun i x -> decided.[i] = -1 || decided.[i] = x)
    |> fun (canBeSelected : int -> int -> bool) ->
      [| 0..2 |]
      |> Array.map (fun x -> if canBeSelected 0 x then 1 else 0)
      |> fun memo0 ->
        [| 0..8 |]
        |> Array.map (
          fun x -> memo1.[x % 3]
            


        )


    |> fun (arr1) ->
      [2..n]
      |> List.fold (
        fun (arr1, arr2) i ->
          [1..3]
          |> List.map (
            fun j ->
              if decided.[i] > 0 && decided.[i] <> j
              then 0
              else (

              )
          )
      ) (arr1, (Array.create 3 0))


|> fun _ -> stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun ns ->
  (
    (ns |> Array.head),
    (ns.[1..(ns.Length - 2)]),
    (ns |> Array.last)
  )
|> fun (n1, ns, ans) ->
  (Array.create 21 0L)
  |> fun memo0 -> (memo0.[n1] <- 1L) |> fun _ -> memo0
  |> fun memo0 ->
    ns
    |> Array.fold (
      fun memo1 ni ->
        (Array.create 21 0L)
        |> fun memo2 ->
          memo1
          |> Array.mapi (
            fun i mi ->
              if mi = 0L
              then ()
              else (
                [ i + ni; i - ni ]
                |> List.filter (fun i2 -> 0 <= i2 && i2 <= 20)
                |> List.iter (fun i2 -> memo2.[i2] <- memo2.[i2] + mi)
              )
          )
          |> fun _ -> memo2
    ) memo0
  |> fun memo -> memo.[ans]
|> printfn "%d"
