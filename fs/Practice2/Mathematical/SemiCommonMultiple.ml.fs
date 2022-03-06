// https://atcoder.jp/contests/abc150/tasks/abc150_d

let _gcd =
  [| fun _ _ -> LanguagePrimitives.GenericZero |]
  |> fun fnc ->
    (
      fun a b ->
        if b = LanguagePrimitives.GenericZero then a else fnc.[0] b (a % b)
    )
    |> fun fn ->
      (fnc.[0] <- fn)
      |> fun () ->
        fun a b -> fn (max a b) (min a b)

let gcd =
  fun a b ->
    ([| (max a b, min a b) |])
    |> fun box ->
      Seq.initInfinite (fun _ -> box.[0])
      |> Seq.takeWhile (fun (_, b) -> b > LanguagePrimitives.GenericZero)
      |> Seq.iter (
        fun (a, b) -> box.[0] <- (b, a % b)
      )
      |> fun _ -> box.[0] |> fst

let lcm = fun a b -> a * b / gcd a b

let count =
  fun m ->
    Seq.reduce lcm
    >> fun l -> ((m <<< 1) / l + 1L) >>> 1

let areSame2s =
  Seq.map (fun x -> x &&& - x)
  >> Set.ofSeq
  >> Set.count
  >> ((=) 1)

// main
(
  (stdin.ReadLine().Split().[1] |> int64),
  (stdin.ReadLine().Split() |> Seq.map int64)
)
|> fun (m, xs) ->
  xs |> areSame2s
  |> fun same ->
    if not same then 0L else
      xs |> count m
|> printfn "%d"

// https://atcoder.jp/contests/abc150/submissions/29920071
