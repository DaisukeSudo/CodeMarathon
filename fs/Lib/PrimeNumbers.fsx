let primeNumbers () =
  let mutable ps = [2L]
  seq {
    yield 2L;
    yield!
      Seq.initInfinite (fun i -> 3L + int64 i * 2L)
      |> Seq.filter (fun x ->
        let ul = x |> double |> sqrt |> ceil |> int64
        ps |> List.filter ((>) ul) |> List.forall (fun p -> x % p <> 0L)
      )
      |> Seq.map (fun x -> ps <- x :: ps; x)
  }
