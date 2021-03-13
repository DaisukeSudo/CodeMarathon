// https://atcoder.jp/contests/joi2014yo/tasks/joi2014yo_d

(
  stdin.ReadLine () |> int,
  stdin.ReadLine ()
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
  [0..n - 1]
  |> List.fold (
    fun (memo : int []) i ->
      [|1..7|]
      |> Array.map (
        fun j ->
          if j &&& rs.[i] <> rs.[i]
          then 0
          else (
            [1..7]
            |> List.map (
              fun k ->
                if [0..2] |> List.exists (
                  fun x -> (j >>> x &&& 1 = 1) && (k >>> x &&& 1 = 1)
                )
                then memo.[k - 1] % 10007
                else 0
            )
            |> List.sum
          )
      )
  ) ([[|1|]; Array.create 6 0] |> Array.concat)
  |> Seq.sum
  |> fun v -> v % 10007
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

// https://atcoder.jp/contests/joi2014yo/submissions/20865427
