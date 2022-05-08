// https://atcoder.jp/contests/typical90/tasks/typical90_f

let n, k = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let s    = stdin.ReadLine()

let cs = Array.zeroCreate 26 in s |> Seq.iter (int >> ((+) -97) >> fun x -> cs.[x] <- cs.[x] + 1)

let nextCur =
  fun cur ->
    let mutable i = cur
    let mutable next = true
    while next do
      if cs.[i] > 0 then
        cs.[i] <- cs.[i] - 1
        next <- false
      else
        i <- i - 1
    i

let reduce =
  fun (s: string) (c: char) ->
    let i = s |> Seq.findIndex ((=) c)
    s.[0..i - 1] + s.[i + 1..]

let res =
  ((s, 25), [1..n - k]) ||> List.fold (fun (acc, cur) _ ->
    let i = nextCur cur in reduce acc (i + 97 |> char), i
  )
  |> fst

printfn "%s" res
