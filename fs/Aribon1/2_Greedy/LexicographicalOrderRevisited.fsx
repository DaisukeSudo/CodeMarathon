// https://atcoder.jp/contests/abc009/tasks/abc009_3

// let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
// let s    = stdin.ReadLine()

// let sorted = new string(s |> Seq.sort |> Seq.toArray)

// let toI = int >> ((+) -97)

// let dict = Array.init 26 (fun _ -> Array.empty) in
//   s |> Seq.map toI |> Seq.iteri (fun i c -> dict.[c] <- dict.[c] |> Array.append [| i |])

// // b b b a a a -> a b b a a b
// // a: [6; 5; 4] -> [5; 4; 1]
// // b: [3; 2; 1] -> [6; 3; 2]

// let swap (s: string) i =
//   let src = s.[i]       // 変更前の文字 b
//   let srcI = toI src
//   let dst = sorted.[i]    // 変更後の文字 a
//   let dstI = toI dst
//   let j = dict.[dstI] |> Array.head
//   dict.[srcI] <- Array.concat [[| j |]; dict.[srcI].[.. dict.[srcI].Length - 2]]
//   dict.[dstI] <- Array.concat [dict.[dstI] |> Array.tail; [| i |]]
//   s.[0 .. i - 1] + string dst + s.[i + 1 .. j - 1] + string src + s.[j + 1 ..]

// let rec loop s i limit =
//   if limit = 0 || s = sorted then
//     s
//   else if s.[i] = sorted.[i] then
//     loop s (i + 1) limit
//   else 
//     loop (swap s i) (i + 1) (limit - 1)

// loop s 0 k
// |> stdout.WriteLine

// ----

let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let s    = stdin.ReadLine()

let swap a b (s: string) =
  let cs = s |> Seq.toArray
  cs.[a] <- s.[b]
  cs.[b] <- s.[a]
  cs |> System.String

let diff (s: string) (t: string) =
  [0 .. s.Length - 1]
  |> List.filter (fun i -> s.[i] <> t.[i])
  |> List.length

(s, [0 .. n - 1])
||> List.fold (fun t i ->
  [i + 1 .. n - 1]
  |> List.filter (fun j -> t.[j] < t.[i])
  |> List.map (fun j -> (j, t.[j]))
  |> List.filter (fun (j, _) ->
      t
      |> swap i j
      |> diff s
      |> ((>=) k)
  )
  |> List.sortBy snd
  |> List.tryHead
  |> Option.map fst
  |> Option.map (fun j -> t |> swap i j)
  |> Option.defaultValue t
)
|> stdout.WriteLine

// https://atcoder.jp/contests/abc009/submissions/56954992
