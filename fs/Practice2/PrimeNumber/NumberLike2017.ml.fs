// https://atcoder.jp/contests/abc084/tasks/abc084_d

let isPrimeNumber = fun (ps: list<int>) (x: int) ->
  x |> double |> sqrt |> int |> fun ul -> ps |> List.forall (fun p -> ul < p || x % p <> 0)

let primeNumbers = fun () ->
  [| [2] |]
  |> fun ps ->
    seq {
      yield 2;
      yield!
        Seq.initInfinite (fun i -> 3 + i * 2)
        |> Seq.filter (fun x -> x |> isPrimeNumber ps.[0])
        |> Seq.map (fun x -> (ps.[0] <- x::ps.[0]) |> fun _ -> x)
    }

let createPrimeNumbers = fun (maxValue: int) ->
  (Array.create maxValue false)
  |> fun memo ->
    primeNumbers ()
    |> Seq.takeWhile ((>) maxValue)
    |> Seq.iter (fun x -> memo.[x] <- true)
    |> fun _ -> memo

let createAccumulator = fun (ps: bool []) ->
  (Array.create ps.Length 0)
  |> fun memo ->
    [2..ps.Length - 1]
    |> List.fold (fun acc i ->
      (i &&& 1 = 1 && ps.[i] && ps.[(i + 1) / 2])
      |> fun c -> (if c then acc + 1 else acc)
      |> fun v -> (memo.[i] <- v) |> fun _ -> v
    ) 0
    |> fun _ -> memo

100000
|> createPrimeNumbers
|> createAccumulator
|> fun acc ->
  stdin.ReadLine()
  |> int
  |> fun q -> [1..q]
  |> Seq.map (fun i ->
    stdin.ReadLine().Split()
    |> Array.map int
    |> fun x -> (x.[0], x.[1])
    |> fun (l, r) -> acc.[r] - acc.[l - 1]
  )
  |> Seq.iter (printfn "%d")

// https://atcoder.jp/contests/abc084/submissions/24527097
