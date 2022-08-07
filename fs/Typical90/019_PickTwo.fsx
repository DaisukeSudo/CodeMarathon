// https://atcoder.jp/contests/typical90/tasks/typical90_s

let n = stdin.ReadLine() |> int
let a = stdin.ReadLine().Split() |> Array.map int

let memo = Array.init (n * 2) (fun _ -> Array.create (n * 2) 0)

seq {
  for i in 0 .. n - 1 do
  for j in 0 .. (n * 2) - (i * 2) - 2 ->
    (j, j + (i * 2) + 1)
}
|> Seq.iter (fun (i, j) ->
  let c1 = (a.[i] - a.[j] |> abs) + memo.[i + 1].[j - 1]
  let c2 = [i + 1..2..j - 2] |> List.map (fun k ->
    memo.[i].[k] + memo.[k + 1].[j]
  )
  memo.[i].[j] <- c1 :: c2 |> List.min
)

memo.[0].[n * 2 - 1] |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/33859733
