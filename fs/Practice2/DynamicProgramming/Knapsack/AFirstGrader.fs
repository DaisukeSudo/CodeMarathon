// https://atcoder.jp/contests/joi2011yo/tasks/joi2011yo_d

stdin.ReadLine()
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


// input:
// 11
// 8 3 2 4 8 7 2 4 0 8 8

// memo:
// [|0;  0;  0;  0;  0;  0;  0;  0;  1;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0;  0|]
// [|0;  0;  0;  0;  0;  1;  0;  0;  0;  0;  0;  1;  0;  0;  0;  0;  0;  0;  0;  0;  0|]
// [|0;  0;  0;  1;  0;  0;  0;  1;  0;  1;  0;  0;  0;  1;  0;  0;  0;  0;  0;  0;  0|]
// [|0;  0;  0;  1;  0;  1;  0;  1;  0;  1;  0;  1;  0;  1;  0;  0;  0;  1;  0;  0;  0|]
// [|0;  1;  0;  1;  0;  1;  0;  0;  0;  1;  0;  1;  0;  1;  0;  1;  0;  1;  0;  1;  0|]
// [|0;  0;  1;  0;  1;  0;  1;  0;  2;  0;  2;  0;  2;  0;  0;  0;  1;  0;  1;  0;  1|]
// [|1;  0;  1;  0;  2;  0;  3;  0;  3;  0;  4;  0;  2;  0;  3;  0;  1;  0;  2;  0;  1|]
// [|2;  0;  3;  0;  4;  0;  5;  0;  4;  0;  6;  0;  4;  0;  6;  0;  3;  0;  3;  0;  1|]
// [|4;  0;  6;  0;  8;  0; 10;  0;  8;  0; 12;  0;  8;  0; 12;  0;  6;  0;  6;  0;  2|]
// [|8;  0; 12;  0;  8;  0; 12;  0; 10;  0; 12;  0; 10;  0; 10;  0;  8;  0; 12;  0;  8|]
//                                   ↑

// output:
// 10


// https://atcoder.jp/contests/joi2011yo/submissions/18506152
