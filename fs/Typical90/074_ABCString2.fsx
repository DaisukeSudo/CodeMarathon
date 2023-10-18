// https://atcoder.jp/contests/typical90/tasks/typical90_bv

let _ = stdin.ReadLine() |> int
let s = stdin.ReadLine() |> Seq.map char

((0, 0L), s) ||> Seq.fold (fun (i, ans) c ->
  (
    i + 1,
    match c with
    | 'b' -> ans + 1L * (1L <<< i)
    | 'c' -> ans + 2L * (1L <<< i)
    | _ -> ans
  )
)
|> snd
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/46715321
