// https://atcoder.jp/contests/pakencamp-2019-day3/tasks/pakencamp_2019_day3_d

(
  stdin.ReadLine() |> int,
  Array.init 5 (fun _ -> stdin.ReadLine() |> Seq.map string |> Seq.toArray),
  Array.create 3 0,
  [| "R"; "B"; "W" |]
)
|> fun (n, sss, memo0, rbw) ->
  [| 0..(n - 1) |]
  |> Array.map (fun j -> sss |> Array.map (fun ss -> ss.[j]))
  |> Array.fold (
    fun memo ss ->
      rbw
      |> Array.mapi (
        fun i s ->
          (
            ss     |> Seq.filter ((<>) s) |> Seq.length,
            [0..2] |> Seq.filter ((<>) i) |> Seq.map (fun j -> memo.[j]) |> Seq.min
          )
          |> fun (cur, acc) -> cur + acc
      )
  ) memo0
  |> Array.min
|> printfn "%A"

// https://atcoder.jp/contests/pakencamp-2019-day3/submissions/19417337
