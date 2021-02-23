// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_2_A

(fun () -> stdin.ReadLine () |> fun x -> x.Split ' ' |> Array.map int)
|> fun readLine ->
  readLine ()
  |> fun arr -> (arr.[0], arr.[1])
  |> fun (v, e) ->
    (
      List.init e (fun _ -> readLine () |> fun arr -> (arr.[0], arr.[1], arr.[2]))
      |> List.map (fun (s, t, d) -> (s, (if t = 0 then v else t), d)),
      Array.init (1 <<< (v + 1)) (fun _ -> Array.create (v + 1) System.Int32.MaxValue)
    )
    |> fun (graph, memo) ->
      (memo.[1].[0] <- 0)
      |> fun () ->
        seq {
          for vx in 1..1 <<< (v + 1) do 
          for (s, t, d) in graph do
          if vx &&& 1 = 1
            && vx >>> s &&& 1 = 1
            && vx >>> t &&& 1 = 1
          then yield (vx, t, d, memo.[vx ^^^ (1 <<< t)].[s])
        }
        |> Seq.filter (
          fun (_, _, _, prev) ->
            prev <> System.Int32.MaxValue
        )
        |> Seq.iter (
          fun (vx, t, d, prev) ->
            memo.[vx].[t] <- min memo.[vx].[t] (prev + d)
        )
      |> fun _ -> memo |> Array.last |> Array.last
      |> fun x -> if x = System.Int32.MaxValue then -1 else x
|> printfn "%d"

// input:
// 4 6
// 0 1 2
// 1 2 3
// 1 3 9
// 2 0 1
// 2 3 6
// 3 2 4

// memo:
// [|
//   [| -;  -;  -;  -;  - |]
//   [| 0;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  2;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  5;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -; 11;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -; 15; 11;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  6 |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -;  - |]
//   [| -;  -;  -;  -; 16 |]
// |]

// output:
// 16
