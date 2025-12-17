// https://atcoder.jp/contests/cf17-final/tasks/cf17_final_d

// もし選ぶ参加者のメンツが決まっているなら
// 最適な並べ方は一通りに決まる
// 
// 2人を連続で積む場合
// 参加者A（身長 H_a, 力 P_a）
// 参加者B（身長 H_b, 力 P_b）
// 現在の座布団の高さ S
// 
// 1. A → B の順で積める条件
//   * Aが積める：S ≦ H_a
//   * Bが積める：S + P_a ≦ H_b
// まとめると S ≦ min(H_a, H_b - P_a)
// 
// 2. B → A の順で積める条件
//   * Bが積める：S ≦ H_b
//   * Aが積める：S + P_b ≦ H_a
// まとめると S ≦ min(H_b, H_a - P_b)
// 
// 許容される S の上限が広い順序を採用する方が有利になる
// つまり H_i + P_i が小さい順に並べるのが最適

// ----

// dp.[k] = k人の参加者が座布団を積むことに成功した時の最小の座布団の高さ

let n  = stdin.ReadLine() |> int
let hp = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])

let dp = Array.init (n + 1) (fun _ -> System.Int32.MaxValue)
dp.[0] <- 0

hp
|> Array.sortBy (fun (h, p) -> h + p)
|> Array.iter (fun (h, p) ->
  for j = n - 1 downto 0 do
    if dp.[j] <= h && dp.[j + 1] > dp.[j] + p then
      dp.[j + 1] <- dp.[j] + p
)

dp
|> Array.findIndexBack ((>) System.Int32.MaxValue)
|> stdout.WriteLine

// https://atcoder.jp/contests/cf17-final/submissions/71772975
