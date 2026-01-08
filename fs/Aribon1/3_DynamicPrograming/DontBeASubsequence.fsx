// https://atcoder.jp/contests/arc081/tasks/arc081_c

let a = stdin.ReadLine()

// dp.[c] = 現在の位置より後ろの範囲において
//          文字 c から始まる「部分列ではない最短・辞書順最小」の文字列
// 初期値：a, b, ... z
let dp = Array.init 26 (fun i -> string (char (int 'a' + i)))

for i = a.Length - 1 downto 0 do
  let c = int a.[i] - int 'a'
  let x = dp |> Array.minBy (fun x -> x.Length)
  dp.[c] <- string a.[i] + x

dp
|> Array.minBy (fun x -> x.Length)
|> stdout.WriteLine

// https://atcoder.jp/contests/arc081/submissions/72292973
