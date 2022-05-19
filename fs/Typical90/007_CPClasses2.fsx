// https://atcoder.jp/contests/typical90/tasks/typical90_g

// let stdin  = new System.IO.StreamReader("007_CPClasses.input")
// let stdout = new System.IO.StreamWriter("007_CPClasses.output")

let solve (a: int []) b =
  let rec bs b li ri =
    if ri - li <= 1 then (li, ri)
    else
      let mi = li + (ri - li) / 2
      if a.[mi] > b
      then bs b li mi
      else bs b mi ri
  
  let li, ri = bs b 0 (a.Length - 1)
  (b - a.[li], a.[ri] - b) ||> min

let _ = stdin.ReadLine() |> ignore
let a = stdin.ReadLine().Split() |> Array.map int |> fun arr -> [arr; [|-1_000_000_000; 2_000_000_001|]] |> Array.concat |> Array.sort
let q = stdin.ReadLine() |> int

Seq.init q ignore |> Seq.iter (stdin.ReadLine >> int >> solve a >> stdout.WriteLine)

// stdout.Close()

// https://atcoder.jp/contests/typical90/submissions/31793524
