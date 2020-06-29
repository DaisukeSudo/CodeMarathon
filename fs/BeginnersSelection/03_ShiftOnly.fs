// https://atcoder.jp/contests/abs/tasks/abc081_b

// stdin.ReadLine() |> ignore
// |> fun () -> stdin.ReadLine()
// |> fun x -> x.Split(' ')
// |> Array.map int
// |> Array.map (fun x -> System.Convert.ToString(x, 2))
// |> Array.map (fun x -> x.Length - x.LastIndexOf('1') - 1)
// |> Array.min
// |> printfn "%d"

// ----------

// stdin.ReadLine() |> ignore |> fun () -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.map (fun x -> System.Convert.ToString(x, 2)) |> Array.map (fun x -> x.Length - x.LastIndexOf('1') - 1) |> Array.min |> printfn "%d"

// https://atcoder.jp/contests/abs/submissions/9414719

// ----------

// const main = input => (
//   x => x.length - x.lastIndexOf('1') - 1
// )(
//   input.split('\n')[1]
//     .split(' ')
//     .map(x => (+x))
//     .reduce((a, x) => a | x)
//     .toString(2)
// );

// stdin.ReadLine()
// |> ignore
// |> fun () -> stdin.ReadLine()
// |> fun x -> x.Split(' ')
// |> Array.map int
// |> Array.reduce (fun a b -> a ||| b)
// |> fun x -> System.Convert.ToString(x, 2)
// |> fun x -> x.Length - x.LastIndexOf('1') - 1
// |> printfn "%d"

// ---------- 

stdin.ReadLine() |> ignore |> fun () -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.reduce (fun a b -> a ||| b) |> fun x -> System.Convert.ToString(x, 2) |> fun x -> x.Length - x.LastIndexOf('1') - 1 |> printfn "%d"

// https://atcoder.jp/contests/abs/submissions/9414731

// ----------

stdin.ReadLine()
|> ignore
|> fun () -> stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> Array.reduce (|||)
|> fun x -> System.Convert.ToString(x, 2)
|> fun x -> x.Length - x.LastIndexOf('1') - 1
|> printfn "%d"
