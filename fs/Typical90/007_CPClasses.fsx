// https://atcoder.jp/contests/typical90/tasks/typical90_g

// let stdin  = new System.IO.StreamReader("007_CPClasses.input")
// let stdout = new System.IO.StreamWriter("007_CPClasses.output")

let _ = stdin.ReadLine() |> ignore
let a = stdin.ReadLine().Split() |> Array.map int |> fun arr -> [arr; [|2_000_000_001|]] |> Array.concat |> Array.sort
let q = stdin.ReadLine() |> int
let b = Array.init q (fun i -> (i, stdin.ReadLine() |> int)) |> Array.sortBy (fun (_, b) -> b)

let ans = Array.create q 0
let mutable ia = 0

b |> Array.iter (fun (ib, b) ->
  while abs (a.[ia] - b) >= abs (a.[ia + 1] - b) do ia <- ia + 1
  ans.[ib] <- abs (a.[ia] - b)
)

ans
|> Array.map string
|> String.concat "\n"
|> stdout.WriteLine

// stdout.Close()

// https://atcoder.jp/contests/typical90/submissions/31713538
// https://atcoder.jp/contests/typical90/submissions/31713671
