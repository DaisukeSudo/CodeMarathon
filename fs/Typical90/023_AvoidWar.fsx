// https://atcoder.jp/contests/typical90/tasks/typical90_w

let guard t arr = Array.concat [ [| t |]; arr; [| t |] ]
let guard2 t h w ilzr = Array.init h (ilzr >> guard t) |> guard (Array.create (w + 2) t)

// ---- 

let p = 1_000_000_007L
let m x = x % p

let white = '.' // let black = '#'

let h, w = () |> (stdin.ReadLine >> (fun x -> x.Split()) >> fun x -> int x.[0], int x.[1])
let cs = (ignore >> stdin.ReadLine >> Seq.map ((=) white) >> Seq.toArray) |> guard2 false h w

let dp = Array.init (h + 2) (fun _ -> Array.create (w + 2) (0L, 0L))
let mutable started = false

[ 1 .. h ] |> List.iter (fun i -> [ 1 .. w ] |> List.iter (fun j ->

  if not started && cs.[i].[j]
  then
    dp.[i].[j] <- (1L, 1L)
    started <- true

  let z1, o1 = dp.[i].[j]
  [(i - 1, j - 1); (i - 1, j); (i - 1, j + 1); (i, j - 1)] |> List.iter (fun (i0, j0) ->

    ()
  )
))

dp.[h].[w] |> fun (z, o) -> z + o |> stdout.WriteLine

printfn "dp: %A" dp
