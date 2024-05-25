// https://atcoder.jp/contests/joi2008yo/tasks/joi2008yo_a

stdin.ReadLine() |> int
|> fun p -> ((0, 1000 - p), [500; 100; 50; 10; 5; 1])
||> List.fold (fun (ans, rem) coin ->
  (rem / coin + ans, rem % coin)
)
|> fst
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2008yo/submissions/53820381
