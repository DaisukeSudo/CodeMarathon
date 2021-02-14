// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1611

Seq.initInfinite (fun _ -> stdin.ReadLine ())
|> Seq.takeWhile ((<>) "0")
|> Seq.fold (
  fun (i, ns, wss) s ->
    if i % 2 = 0
    then (
      s
      |> int
      |> fun n -> (i + 1, n :: ns, wss)
    )
    else (
      s.Split(' ')
      |> Array.map int
      |> fun ws -> (i + 1, ns, ws :: wss)
    )
) (0, [], [])
|> fun (_, ns, wss) -> (List.zip ns wss)
|> List.rev
|> List.map (
  fun (n, ws) ->
    (
      (fun a b -> ws.[a] - ws.[b] |> abs |> ((>=) 1)),
      (Array.init n (fun _ -> Array.create n true)),
      (Array.init n (fun _ -> Array.create n 0))
    )
    |> fun (isR, memR, memC) ->
      (
        fun s e ->
          (
            match e - s with
            | x when x % 2 = 0 -> false
            | 1 -> isR s e
            | _ ->
              (isR s e && memR.[s + 1].[e - 1])
                || (memR.[s].[s + 1] && memR.[s + 2].[e])
          )
          |> fun x -> (memR.[s].[e] <- x) |> fun () -> x
      )
      |> fun fnR ->
        [1..(n - 1)]
        |> List.map (
          fun i ->
            [for j in 0..(n - i - 1) -> (j, j + i)]
            |> List.map (
              fun (s, e) ->
                (
                  if fnR s e
                  then (e - s + 1)
                  else
                    [for k in s..(e - 1) -> ((s, k), (k + 1, e))]
                    |> List.map (
                      fun ((s1, e1), (s2, e2)) ->
                        memC.[s1].[e1] + memC.[s2].[e2]
                    )
                    |> List.max
                )
                |> fun x -> memC.[s].[e] <- x
            )
        )
      |> fun _ -> memC.[0].[n - 1]
)
|> List.iter (printfn "%d")

// ----

// input:
// 14
// 8 7 1 4 3 5 4 1 6 8 10 4 6 5
// 0

// memC:
// [|
//   [| 0;  2;  2;  2;  4;  4;  6;  8;  8; 10; 10; 10; 10; 12 |];
//   [| 0;  0;  0;  0;  2;  2;  4;  6;  8;  8;  8;  8;  8; 10 |];
//   [| 0;  0;  0;  0;  2;  2;  4;  6;  6;  6;  6;  6;  6;  8 |];
//   [| 0;  0;  0;  0;  2;  2;  4;  4;  4;  4;  4;  4;  4;  6 |];
//   [| 0;  0;  0;  0;  0;  0;  2;  2;  2;  2;  2;  2;  2;  4 |];
//   [| 0;  0;  0;  0;  0;  0;  2;  2;  2;  2;  2;  2;  2;  4 |];
//   [| 0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  2 |];
//   [| 0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  2 |];
//   [| 0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  2 |];
//   [| 0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  2 |];
//   [| 0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  2 |];
//   [| 0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  2 |];
//   [| 0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  2 |];
//   [| 0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0 |];
// |]

// output:
// 12
