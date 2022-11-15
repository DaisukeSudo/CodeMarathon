// https://atcoder.jp/contests/typical90/tasks/typical90_ae

let grundy = Array.init 51 (fun _ -> Array.create 1400 0)

[0 .. 50] |> List.iter (fun i -> [0 .. 1325] |> List.iter (fun j ->
  let mex = Array.create 1400 false

  if i >= 1 then
    mex.[grundy.[i - 1].[j + i]] <- true

  if j >= 2 then
    [1 .. j / 2] |> List.iter (fun k -> mex.[grundy.[i].[j - k]] <- true)

  let rec loop k =
    if not mex.[k] then
      grundy.[i].[j] <- k
    else
      loop (k + 1)

  loop 0
))

// ----

let _ = stdin.ReadLine() |> int
let ws = stdin.ReadLine().Split() |> Array.map int
let bs = stdin.ReadLine().Split() |> Array.map int

Array.zip ws bs
|> Array.map (fun (wi, bi) -> grundy.[wi].[bi])
|> Array.reduce (^^^)
|> fun x -> if x <> 0 then "First" else "Second"
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/36514927
