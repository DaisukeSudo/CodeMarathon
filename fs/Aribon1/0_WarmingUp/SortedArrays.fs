// https://atcoder.jp/contests/agc013/tasks/agc013_a

stdin.ReadLine()
|> ignore
|> fun () -> stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun arr ->
  arr
  |> Seq.fold (fun (prev, d, s, ret) cur ->
    match prev with
    | _ when prev < cur && d > 0 ->
      (cur,  1, s |> not, ret + if s then 0 else 1)
    | _ when prev > cur && d < 0 ->
      (cur, -1, s |> not, ret + if s then 0 else 1)
    | _ -> (cur, cur - prev, false, ret)
  ) (arr.[0], 0, false, 0)
|> fun (_, _, _, ret) -> ret
|> printfn "%d"

// ----

//   1 2 3 2 1 1 2 1 2 1
// 1 1 2 3 2 1 1 2 1 2 1 1
//   f f t f f t t t t f 
//        |     |   |     |

stdin.ReadLine()
|> ignore
|> fun () -> stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun arr ->
  Array.foldBack (
    fun (i, x) acc ->
      if i > 0 && arr.[i - 1] = x
      then acc
      else x::acc
  ) (arr |> Array.mapi (fun i x -> i, x)) []
  |> Seq.toArray
|> fun arr ->
  Array.concat [[| arr.[0] |]; arr; [| arr.[arr.Length - 1] |]]
|> fun arr ->
  arr
  |> Array.mapi (fun i x -> i, x)
  |> fun arri ->
    arri.[1..arr.Length - 2]
    |> Seq.fold (
      fun (c, ret) (i, x) ->
        (arr.[i - 1], arr.[i + 1])
        |> fun (p, n) -> c > 0 && ((x < p && x < n) || (x > p && x > n))
        |> fun sw -> if sw then (0, ret + 1) else (c + 1, ret)
    ) (0, 1)
|> fun (_, ret) -> ret
|> printfn "%d"

// https://atcoder.jp/contests/agc013/submissions/11487453
