// https://atcoder.jp/contests/abc138/tasks/abc138_d

stdin.ReadLine()
|> fun x -> x.Split() |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (n, q) ->
  (
    List.init (n - 1) (fun _ -> stdin.ReadLine() |> fun x -> x.Split() |> Array.map int |> fun x -> (x.[0], x.[1])),
    List.init q       (fun _ -> stdin.ReadLine() |> fun x -> x.Split() |> Array.map int |> fun x -> (x.[0], x.[1]))
  )
  |> fun (nabs, qpxs) ->
    (
      (
        (Array.create (n + 1) [])
        |> fun (edges: int list array) ->
          nabs
          |> List.iter (
            fun  (a, b) ->
              (
                (edges.[a] <- b::edges.[a]),
                (edges.[b] <- a::edges.[b])
              )
              |> ignore
          )
          |> fun () -> edges
      ),
      (
        (Array.zeroCreate (n + 1))
        |> fun (points: int array) ->
          qpxs
          |> List.iter (
            fun (p, x) ->
              (points.[p] <- points.[p] + x)
          )
          |> fun () -> points
      )
    )
  |> fun (edges, points) ->
    (
      [| [(0, 1)] |],
      Array.zeroCreate (n + 1)
    )
    |> fun ((stack: (int * int) list array), (ans: int array)) ->
      Seq.initInfinite (
        fun _ ->
          match stack.[0] with
          | [] -> ()
          | (pi, ci)::ts ->
            (ans.[ci] <- ans.[pi] + points.[ci])
            |> fun () ->
              match edges.[ci] with
              | [] -> (stack.[0] <- ts)
              | cs ->
                cs
                |> List.filter ((<>) pi)
                |> List.map (fun i -> (ci, i))
                |> fun hs -> (stack.[0] <- (hs @ ts))
      )
      |> Seq.takeWhile (fun _ -> stack.[0] |> List.isEmpty |> not)
      |> Seq.length
    |> fun _ -> ans
|> fun ans ->
  ans
  |> Array.skip 1
  |> Array.map string
  |> String.concat " "
|> printfn "%s"

// -----

// 4 3
// 1 4
// 1 3
// 2 3
// 2 10
// 1 100
// 3 1

// edges: [|[]; [3; 4]; [3]; [2; 1]; [1]|]
// points: [|0; 100; 10; 1; 0|]
// ans: [|0; 0; 0; 0; 0|]

// pi: 0, ci: 1, stack: [(0, 1)]
//   ans: [|0; 100; 0; 0; 0|]
//   cs: [3; 4]
// pi: 1, ci: 3, stack: [(1, 3); (1, 4)]
//   ans: [|0; 100; 0; 101; 0|]
//   cs: [2; 1]
// pi: 3, ci: 2, stack: [(3, 2); (3, 1); (1, 4)]
//   ans: [|0; 100; 111; 101; 0|]
//   cs: [3]
// pi: 1, ci: 4, stack: [(1, 4)]
//   ans: [|0; 100; 111; 101; 100|]
//   cs: [1]

// 100 111 101 100

// -----

// https://atcoder.jp/contests/abc138/submissions/16536816
