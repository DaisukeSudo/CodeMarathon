let imos w h lrs =
  let memo = Array.init h (fun _ -> Array.create w 0)

  let inline inc x y = memo.[y].[x] <- memo.[y].[x] + 1
  let inline dec x y = memo.[y].[x] <- memo.[y].[x] - 1

  lrs |> Seq.iter (fun (lx, ly, rx, ry) ->
    let rx = rx + 1
    let ry = ry + 1
    let cw = rx < w
    let ch = ry < h
    inc lx ly
    if cw then dec rx ly
    if ch then dec lx ry
    if cw && ch then inc rx ry
  )

  let inline slideW (x, y) = memo.[y].[x] <- memo.[y].[x] + memo.[y].[x - 1]
  let inline slideH (x, y) = memo.[y].[x] <- memo.[y].[x] + memo.[y - 1].[x]

  seq { for x in 1 .. w - 1 do for y in 0 .. h - 1 -> (x, y) } |> Seq.iter slideW
  seq { for x in 0 .. w - 1 do for y in 1 .. h - 1 -> (x, y) } |> Seq.iter slideH

  memo

// ----

imos 6 6 [(0, 0, 3, 3); (2, 2, 5, 5); (1, 3, 2, 4)] |> Array.iter (printfn "%A")
;;

// [|1; 1; 1; 1; 0; 0|]
// [|1; 1; 1; 1; 0; 0|]
// [|1; 1; 2; 2; 1; 1|]
// [|1; 2; 3; 2; 1; 1|]
// [|0; 1; 2; 1; 1; 1|]
// [|0; 0; 1; 1; 1; 1|]
