// https://atcoder.jp/contests/arc006/tasks/arc006_3

open System.Collections.Generic

let n = stdin.ReadLine() |> int
let ws = [1 .. n] |> List.map (fun _ -> stdin.ReadLine() |> int)

let mutable qs = SortedSet<int>()
ws |> List.iter (fun w ->
  qs.GetViewBetween(w, System.Int32.MaxValue)
  |> Seq.tryHead
  |> Option.iter (qs.Remove >> ignore)
  qs.Add(w) |> ignore
) 

qs.Count |> stdout.WriteLine

// https://atcoder.jp/contests/arc006/submissions/57353817
