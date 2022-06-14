// https://atcoder.jp/contests/typical90/tasks/typical90_k

let n = stdin.ReadLine() |> int
let dcs =
  Array.init n (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int64 x.[2])
  |> Array.sort
  |> fun arr -> [[|(0, 0, 0L)|]; arr]
  |> Array.concat

let dp = Array.init (n + 2) (fun _ -> Array.create 10000 0L)

[1..n] |> List.iter (fun i ->       // 仕事
  [0..5000] |> List.iter (fun j ->  // 日程
    dp.[i].[j] <- max dp.[i].[j] dp.[i - 1].[j]

    let d, c, s = dcs.[i]
    if j + c <= d then
      dp.[i].[j + c] <- max dp.[i].[j + c] (dp.[i - 1].[j] + s)
  )
)

dp.[n]
|> Array.max
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/32462571
