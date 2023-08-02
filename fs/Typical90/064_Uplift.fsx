// https://atcoder.jp/contests/typical90/tasks/typical90_bl

let n, q = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let a    = stdin.ReadLine().Split() |> Array.map int64
let lrv  = Array.init q (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int64 x.[2])

let d = [| 0 .. n - 2 |] |> Array.map (fun i -> a.[i + 1] - a.[i])

(d |> Array.sumBy abs, lrv) ||> Array.scan (fun ans (l, r, v) ->
  let ld =
    if l < 2 then 0L else
      let v1 = d.[l - 2]
      let v2 = v1 + v
      d.[l - 2] <- v2
      (abs v2) - (abs v1)

  let rd =
    if r > n - 1 then 0L else
      let v1 = d.[r - 1]
      let v2 = v1 - v
      d.[r - 1] <- v2
      (abs v2) - (abs v1)

  ans + ld + rd
)
|> Array.tail
|> Array.iter stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/44183560
