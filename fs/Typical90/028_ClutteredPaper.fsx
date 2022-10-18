// https://atcoder.jp/contests/typical90/tasks/typical90_ab

let imos w h lrs =
  let memo = Array.init h (fun _ -> Array.create w 0)

  let inline inc x y = memo.[y].[x] <- memo.[y].[x] + 1
  let inline dec x y = memo.[y].[x] <- memo.[y].[x] - 1

  lrs |> Seq.iter (fun (lx, ly, rx, ry) ->
    inc lx ly
    dec rx ly
    dec lx ry
    inc rx ry
  )

  let inline slideW (x, y) = memo.[y].[x] <- memo.[y].[x] + memo.[y].[x - 1]
  let inline slideH (x, y) = memo.[y].[x] <- memo.[y].[x] + memo.[y - 1].[x]

  seq { for x in 1 .. w - 1 do for y in 0 .. h - 1 -> (x, y) } |> Seq.iter slideW
  seq { for x in 0 .. w - 1 do for y in 1 .. h - 1 -> (x, y) } |> Seq.iter slideH

  memo

// ----

let n = stdin.ReadLine() |> int
let cs = seq { for _ in 1 .. n -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1], x.[2], x.[3] }

let ret = Array.create (n + 1) 0

cs
|> imos 1001 1001 
|> Array.iter (Array.iter (fun x -> ret.[x] <- ret.[x] + 1))

ret
|> Array.skip 1
|> Array.iter stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/35763946
