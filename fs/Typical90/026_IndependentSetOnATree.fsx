// https://atcoder.jp/contests/typical90/tasks/typical90_z

let n = stdin.ReadLine() |> int

let es = seq { for _ in 1 .. n - 1 -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1] }

let adjacencies =
  let a = Array.create (n + 1) []
  es |> Seq.iter (fun (s, e) ->
    a.[s] <- e :: a.[s]
    a.[e] <- s :: a.[e]
  )
  a

let group = Array.init 2 (fun _ -> [])

let queue = new System.Collections.Generic.Queue<(int * int * int)>();
queue.Enqueue((0, 0, 1))
while queue.Count > 0 do
  let (g, p, x) = queue.Dequeue()
  group.[g] <- x :: group.[g]
  adjacencies.[x]
  |> List.filter ((<>) p)
  |> List.iter (fun x2 -> queue.Enqueue((g + 1) % 2, x, x2))

group
|> Array.maxBy (List.length)
|> List.take (n / 2)
|> List.map string
|> String.concat " "
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/35338476
