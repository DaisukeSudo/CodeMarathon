// https://atcoder.jp/contests/typical90/tasks/typical90_au

let n = stdin.ReadLine() |> int
if n > 2000 then failwith "imposible"

let convert x = match x with | 'R' -> 0 | 'G' -> 1 | 'B' -> 2 | _ -> failwith "unexpected"

let s = stdin.ReadLine() |> Seq.map convert |> Seq.toArray
let t = stdin.ReadLine() |> Seq.map convert |> Seq.toArray

let mix a b = [[0; 2; 1]; [2; 1; 0]; [1; 0; 2]].[a].[b]

let lines = [| 0; 1; 2 |] |> Array.map (fun x -> Array.map (mix x) t)

let matrix = s |> Array.map (fun x -> lines.[x])

[
  [1 .. n] |> List.map (fun i ->
    [0 .. n - i] |> List.map (fun j -> (j + i, j + 1))
  )
  [1 .. n - 1] |> List.map (fun i ->
    [1 .. n - i] |> List.map (fun j -> (j, i + j))
  )
]
|> List.concat
|> List.map (List.map (fun (a, b) -> matrix.[a - 1].[b - 1]))
|> List.filter (Set.ofList >> Set.count >> ((=) 1))
|> List.length
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/39962042
