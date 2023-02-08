// https://atcoder.jp/contests/typical90/tasks/typical90_ap

let k = stdin.ReadLine() |> int

let calc =
  let dp = Array.create (k + 1) 0
  dp.[0] <- 1
  [1 .. k] |> List.iter (fun i ->
    dp.[i] <-
      [i - 1 .. -1 .. i - (min i 9)]
      |> List.map (fun j -> dp.[j])
      |> List.reduce (fun a x -> (a + x) % 1_000_000_007)
  )
  dp.[k]

if k % 9 = 0 then calc else 0
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/38714470
