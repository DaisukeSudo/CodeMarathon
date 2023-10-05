// https://atcoder.jp/contests/typical90/tasks/typical90_bt

let h, w = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let c = Array.init h (fun _ -> stdin.ReadLine() |> Seq.map ((=) '.') |> Seq.toArray)

let search si sj =
  let mutable ans = 0
  let visited = Array.init h (fun _ -> Array.create w false)

  let rec loop ci cj sum =
    if visited.[si].[sj]
    then
      ans <- max ans sum
    else
      [(ci - 1, cj); (ci + 1, cj); (ci, cj - 1); (ci, cj + 1)]
      |> List.filter (fun (ni, nj) ->
        ni >= 0 && ni < h && nj >= 0 && nj < w
          && c.[ni].[nj]
          && not visited.[ni].[nj]
      )
      |> List.iter (fun (ni, nj) ->
        visited.[ni].[nj] <- true
        loop ni nj (sum + 1)
        visited.[ni].[nj] <- false
      )

  loop si sj 0
  ans

seq {
  for i in 0 .. h - 1 do
  for j in 0 .. w - 1 -> search i j
}
|> Seq.max
|> fun x -> if x < 3 then -1 else x
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/46227720
