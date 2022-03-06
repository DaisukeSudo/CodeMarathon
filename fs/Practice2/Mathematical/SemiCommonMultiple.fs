// https://atcoder.jp/contests/abc150/tasks/abc150_d

(
  fun a b -> ([| (max a b, min a b) |]) |> fun box -> Seq.initInfinite (fun _ -> box.[0]) |> Seq.takeWhile (fun (_, b) -> b > LanguagePrimitives.GenericZero) |> Seq.iter (fun (a, b) -> box.[0] <- (b, a % b)) |> fun _ -> box.[0] |> fst
)
|> fun gcd ->
  (
    (stdin.ReadLine().Split().[1] |> int64),
    (stdin.ReadLine().Split() |> Seq.map int64)
  )
  |> fun (m, xs) ->
    xs
    |> Seq.map (fun x -> x &&& - x) |> Set.ofSeq |> Set.count |> ((=) 1)
    |> fun same ->
      if not same then 0L else
        xs
        |> Seq.reduce (fun a b -> a * b / gcd a b)
        |> fun l -> ((m <<< 1) / l + 1L) >>> 1
|> printfn "%d"

// https://atcoder.jp/contests/abc150/submissions/29920173
