// https://atcoder.jp/contests/joi2014yo/tasks/joi2014yo_d

(
  stdin.ReadLine () |> int,
  "_" + stdin.ReadLine ()
  |> Seq.map (
    fun c ->
      match c with
      | 'J' -> 1
      | 'O' -> 2
      | 'I' -> 4
      | _   -> 0
  )
  |> Seq.toArray
)
|> fun (n, rs) ->
  Array.init (n + 1) (fun _ -> Array.create 8 0)
  |> fun memo ->
    (memo.[0].[1] <- 1)
    |> fun () ->
      seq {
        for i in 1..n do
        for j in 1..7 do
        if (j &&& rs.[i] = rs.[i])
        then yield! seq {
          for j2 in 1..7 do
          if (memo.[i - 1].[j2] > 0)
            && [0..2] |> List.exists (fun x -> (j >>> x &&& 1 = 1) && (j2 >>> x &&& 1 = 1))
          then yield (i, j, memo.[i - 1].[j2])
        }
      }
      |> Seq.iter (
        fun (i, j, v) ->
          memo.[i].[j] <- memo.[i].[j] + v % 10007
      )
      |> fun _ -> memo.[n] |> Array.sum |> fun v -> v % 10007
|> printfn "%d"

// ----------

// i in 1..n

// j: IOJ
// 0: 000
// 1: 001
// 2: 010
// 3: 011
// 4: 100
// 5: 101
// 6: 110
// 7: 111

// ----------

// input:
// 2
// OI

// memo:
// [|
//   [|0; 1; 0; 0; 0; 0; 0; 0|]
//   [|0; 0; 0; 1; 0; 0; 0; 1|]
//   [|0; 0; 0; 0; 1; 2; 2; 2|]
// |]

// output:
// 7

// https://atcoder.jp/contests/joi2014yo/submissions/20780648
