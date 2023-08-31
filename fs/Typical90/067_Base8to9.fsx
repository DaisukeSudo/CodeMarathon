// https://atcoder.jp/contests/typical90/tasks/typical90_bo

let n, k = stdin.ReadLine().Split() |> fun x -> x.[0], int x.[1]

let base8To9 n =
  if n = "0" then n else
    let mutable x = System.Convert.ToInt64(n, 8)
    let mutable a = "";
    while x > 0L do
      a <- string (x % 9L) + a
      x <- x / 9L
    a

let replace8to5 (n: string) = n.Replace('8', '5')

(n, [1 .. k]) ||> List.fold (fun a _ -> a |> base8To9 |> replace8to5)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/45078173
