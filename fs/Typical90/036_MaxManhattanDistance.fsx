// https://atcoder.jp/contests/typical90/tasks/typical90_aj

// let ps = [|(0, 0); (0, 2); (0, 4); (1, 1); (1, 3); (2, 0); (2, 2); (2, 4); (3, 1); (3, 3); (4, 0); (4, 2); (4, 4)|]
// let qs = [|0; 2; 10; 12|]

let n, q = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let ps = [|1 .. n|] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1])
let qs = [|1 .. q|] |> Array.map (fun _ -> stdin.ReadLine() |> int |> ((+) -1))

let p1 = (( 1000000000L,  1000000000L), ps) ||> Seq.fold (fun (x1, y1) (x2, y2) -> if (x1 - x2) + (y1 - y2) > 0L then (x2, y2) else (x1, y1)) 
let p2 = (( 1000000000L, -1000000000L), ps) ||> Seq.fold (fun (x1, y1) (x2, y2) -> if (x1 - x2) + (y2 - y1) > 0L then (x2, y2) else (x1, y1)) 
let p3 = ((-1000000000L,  1000000000L), ps) ||> Seq.fold (fun (x1, y1) (x2, y2) -> if (x2 - x1) + (y1 - y2) > 0L then (x2, y2) else (x1, y1)) 
let p4 = ((-1000000000L, -1000000000L), ps) ||> Seq.fold (fun (x1, y1) (x2, y2) -> if (x2 - x1) + (y2 - y1) > 0L then (x2, y2) else (x1, y1)) 

qs |> Seq.map (fun qi ->
  ps.[qi] |> fun (x1, y1) ->
    [p1; p2; p3; p4] |> List.map (fun (x2, y2) ->
      (abs (x1 - x2)) + (abs (y1 - y2))
    )
    |> List.max
)
|> Seq.iter (stdout.WriteLine)

// https://atcoder.jp/contests/typical90/submissions/37318869
