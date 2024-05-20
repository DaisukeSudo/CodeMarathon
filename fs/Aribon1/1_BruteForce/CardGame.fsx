// https://yukicoder.me/problems/199

let permute =
  let rec loop1 x = function
    | []              -> [[x]]
    | (y :: ys) as xs -> (x :: xs) :: (List.map (fun x -> y :: x) (loop1 x ys))
  let rec loop2 = function
    | []      -> seq [List.empty]
    | x :: xs -> Seq.collect (loop1 x) (loop2 xs)
  loop2

let multiplyZip arr1 arr2 = arr1 |> Seq.collect (fun x1 -> arr2 |> Seq.map (fun x2 -> (x1, x2)))

// ----

let _ = stdin.ReadLine() |> int
let a = stdin.ReadLine().Split() |> Seq.map int |> Seq.toList
let b = stdin.ReadLine().Split() |> Seq.map int |> Seq.toList

let pa = a |> permute // [[2; 4]; [4; 2]]
let pb = b |> permute // [[1; 3]; [3; 1]]

multiplyZip pa pb
// [([2; 4], [1; 3]); ([2; 4], [3; 1]); ([4; 2], [1; 3]); ([4; 2], [3; 1])]
|> Seq.map (fun (a, b) -> Seq.zip a b)
// [[(2, 1); (4, 3)]; [(2, 3); (4, 1)]; [(4, 1); (2, 3)]; [(4, 3); (2, 1)]]
|> Seq.map (Seq.sumBy (fun (a, b) -> if a > b then 1 else -1))
// [2; 0; 0; 2]
|> Seq.averageBy (fun x -> if x > 0 then 1. else 0.)
// 0.5
|> printfn "%A"

// https://yukicoder.me/submissions/982973
