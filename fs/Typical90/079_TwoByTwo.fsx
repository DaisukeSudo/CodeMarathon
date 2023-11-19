// https://atcoder.jp/contests/typical90/tasks/typical90_ca

let h, w = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let a = Array.init h (fun _ -> stdin.ReadLine().Split() |> Array.map int64)
let b = Array.init h (fun _ -> stdin.ReadLine().Split() |> Array.map int64)

let m = Array.init h (fun _ -> Array.create w 0L)
let mutable ans = 0L

[0 .. h - 1] |> List.iter (fun x -> [0 .. w - 1] |> List.iter (fun y ->
  m.[x].[y] <- b.[x].[y] - a.[x].[y]
))

[0 .. h - 2] |> List.iter (fun x -> [0 .. w - 2] |> List.iter (fun y ->
  let d = m.[x].[y]
  ans <- ans + abs d
  [(x, y); (x + 1, y); (x, y + 1); (x + 1, y + 1)] |> List.iter (fun (x, y) ->
    m.[x].[y] <- m.[x].[y] - d
  )
))

let satisfied = m |> Array.exists (Array.exists ((<>) 0L)) |> not
if satisfied then
  printfn "Yes\n%d" ans
else
  printfn "No"

// https://atcoder.jp/contests/typical90/submissions/47748573
