// https://atcoder.jp/contests/joi2010yo/tasks/joi2010yo_d

let permuteN n xs =
  let rec dist e = function
    | []    -> [[e]]
    | x :: xs -> (e :: x :: xs) :: [for ys in dist e xs -> x :: ys]
  let rec _permuteN n xs =
    match (n, xs) with
    | (0, _)       -> [[]]
    | (_, [])      -> []
    | (_, x :: xs) -> (_permuteN n xs) @ (List.collect (dist x) (_permuteN (n - 1) xs))
  _permuteN n xs

// ----

let n = stdin.ReadLine() |> int
let k = stdin.ReadLine() |> int
let cs = Array.init n (fun _ -> stdin.ReadLine()) |> Array.toList

(Set.empty, permuteN k cs) ||> List.fold (fun acc c ->
  c |> String.concat "" |> acc.Add
)
|> Seq.length
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2010yo/submissions/53226385
