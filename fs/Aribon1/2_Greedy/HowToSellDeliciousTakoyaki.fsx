 // https://atcoder.jp/contests/abc005/tasks/abc005_3

let t  = stdin.ReadLine() |> int
let _, a = stdin.ReadLine(), stdin.ReadLine().Split() |> Array.map int
let _, b = stdin.ReadLine(), stdin.ReadLine().Split() |> Array.map int

let mutable ans = Option.None
let mutable al = a |> Array.toList
let mutable bl = b |> Array.toList

while ans |> Option.isNone do
  if bl = [] then
    ans <- Option.Some true
  else if al = [] then
    ans <- Option.Some false
  else
    let a0 = al |> List.head
    let b0 = bl |> List.head

    if a0 <= b0 && a0 >= b0 - t then
      bl <- bl |> List.tail
    al <- al |> List.tail

ans
|> Option.map (fun x -> if x then "yes" else "no")
|> Option.iter stdout.WriteLine

// https://atcoder.jp/contests/abc005/submissions/57623573
