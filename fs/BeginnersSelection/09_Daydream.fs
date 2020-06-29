// https://atcoder.jp/contests/abs/tasks/arc065_a

// stdin.ReadLine()
// |> fun input ->
//   ["eraser"; "erase"; "dreamer"; "dream"]
//   |> List.fold (
//     fun acc word ->
//       acc
//       |> List.map (
//         fun (x: string) ->
//           x.Split([| word |], System.StringSplitOptions.None)
//           |> Array.filter (fun x -> not (System.String.IsNullOrEmpty(x)))
//           |> Array.toList
//       )
//       |> List.fold (fun a b -> a @ b) []
//   ) [input]
//   |> List.length
//   |> fun x -> (if x = 0 then "YES" else "NO")
//   |> printfn "%s"

// ----------

// stdin.ReadLine() |> fun input -> ["eraser"; "erase"; "dreamer"; "dream"] |> List.fold (fun acc word -> acc |> List.map (fun (x: string) -> x.Split([| word |], System.StringSplitOptions.None) |> Array.filter (fun x -> not (System.String.IsNullOrEmpty(x))) |> Array.toList) |> List.fold (fun a b -> a @ b) []) [input] |> List.length |> fun x -> (if x = 0 then "YES" else "NO") |> printfn "%s"

// https://atcoder.jp/contests/abs/submissions/9414864

// JS より遅い。。
// https://atcoder.jp/contests/abs/tasks/arc065_a

// ----------

stdin.ReadLine()
|> fun input ->
  ["eraser"; "erase"; "dreamer"; "dream"]
  |> List.fold (
    fun acc word ->
      acc
      |> List.collect (
        fun (x: string) ->
          x.Split([| word |], System.StringSplitOptions.None)
          |> Array.filter (System.String.IsNullOrEmpty >> not)
          |> Array.toList
      )
  ) [input]
  |> List.length
  |> fun x -> (if x = 0 then "YES" else "NO")
  |> printfn "%s"
