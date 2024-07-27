// https://atcoder.jp/contests/abc076/tasks/abc076_c

let s' = stdin.ReadLine()
let t  = stdin.ReadLine()

[s'.Length - t.Length .. (-1) .. 0] |> List.tryFind (fun i ->
  [0 .. t.Length - 1] |> List.forall (fun j ->
    s'.[i + j] = '?' || s'.[i + j] = t.[j]
  )
)
|> Option.map (fun i ->
  s'.[0 .. i - 1] + t + s'.[i + t.Length ..]
)
|> Option.map (fun s ->
  s.Replace('?', 'a')
)
|> Option.defaultValue "UNRESTORABLE"
|> stdout.WriteLine

// https://atcoder.jp/contests/abc076/submissions/55992274
