// https://atcoder.jp/contests/typical90/tasks/typical90_f

let k = stdin.ReadLine().Split() |> fun x -> int x.[1]
let s = stdin.ReadLine()

((0, []), [0..k - 1]) ||> List.fold (fun (ofs, acc) i ->
  let mutable isContinued = true
  let mutable mi = -1
  let mutable mc = '{'
  seq {ofs..s.Length - k + i}
  |> Seq.takeWhile (fun _ -> isContinued)
  |> Seq.iter (fun j ->
    if s.[j] < mc then mi <- j; mc <- s.[j]
    if s.[j] = 'a' then isContinued <- false
  )
  (mi + 1, mc :: acc)
)
|> snd
|> List.rev
|> List.map string
|> String.concat ""
|> printfn "%s"

// https://atcoder.jp/contests/typical90/submissions/31605691
