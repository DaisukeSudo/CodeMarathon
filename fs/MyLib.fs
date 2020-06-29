// ----- 入力 -----

// 1行目を捨てる
stdin.ReadLine()
|> ignore
|> fun () -> stdin.ReadLine()
|> printfn "%A"

// 数字列 → 1文字ずつの数値のリスト
stdin.ReadLine()
|> Seq.map (string >> int)
|> Seq.toList
|> printfn "%A"

// 空白区切り → 数値のリスト
stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Seq.map int
|> Seq.toList
|> printfn "%A"

// Ｎ件の改行区切り → 数値のリスト
stdin.ReadLine()
|> int
|> fun n -> [1..n]
|> List.map (fun _ -> stdin.ReadLine() |> int)
|> printfn "%A"

// Ｎ件の改行区切り＋各行空白区切り → 数値のタプルのリスト
stdin.ReadLine()
|> int
|> fun n -> [1..n]
|> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
|> Seq.sortWith (fun (x1, _) (x2, _) -> x1 - x2)
|> printfn "%A"

// 空白区切り → 任意の個数のタプル
stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1], x.[2], x.[3])
|> fun (a, b, c, d) -> (a, b, c, d)
|> printfn "%A"


// ----- シーケンス -----

// 基本
seq {1..9}
|> Seq.fold (fun acc cur -> cur :: acc) []
|> Seq.sortWith (-)
|> Seq.sortWith (fun a b -> b - a)
|> Seq.rev
|> Seq.distinct
|> Seq.length
|> printfn "%d"

// 平均値
let mean =
  fun list ->
    list |> Seq.averageBy float |> int

// 中央値
let median =
  fun list ->
    (
      list |> Seq.sort |> Seq.toArray,
      list |> Seq.length
    )
    |> fun (arr, len) ->
      if len % 2 = 0
      then (arr.[len / 2] + arr.[len / 2 + 1]) / 2
      else arr.[len / 2]

// 最小値またはデフォルト
let minOrDefault : 'a -> seq<'a> -> 'a =
  fun state arr ->
    if (arr |> Seq.length > 0) then (arr |> Seq.min) else state

// 最大値またはデフォルト
let maxOrDefault : 'a -> seq<'a> -> 'a =
  fun state arr ->
    if (arr |> Seq.length > 0) then (arr |> Seq.max) else state

// index 付き fold
let foldi : ('a -> int -> 'b -> 'a) -> 'a -> seq<'b> -> 'a =
  fun folder state arr ->
    arr
    |> Seq.mapi (fun i x -> i, x)
    |> Seq.fold (fun acc (i, x) -> folder acc i x) state


// 組み合わせ：ペア
let combinePair : seq<'a> -> seq<'b> -> seq<'a * 'b> =
  fun arr1 arr2 ->
    arr1
    |> Seq.collect (
      fun x1 ->
        arr2
        |> Seq.map (fun x2 -> (x1, x2))
    )

// 組み合わせ（from < to）
let combinePairHalf : seq<'a> -> seq<'a> -> seq<'a * 'a> =
  fun fromArr toArr ->
    fromArr
    |> Seq.collect (
      fun x1 ->
        toArr
        |> Seq.filter (fun x2 -> x1 < x2)
        |> Seq.map (fun x2 -> (x1, x2))
    )

// 組み合わせ：ペア：サンプル
combinePair [1..9] [1..9]
// |> Seq.filter (fun (a, b) -> f1 (a, b) <> f2 (a, b))
|> Seq.iter (printfn "%A")

// 組み合わせ：順列
let combineSequence =
  fun list ->
    list
    |> List.tail
    |> List.fold (
      fun acc i ->
        acc
        |> List.collect (
          fun cs ->
            list
            |> List.except cs
            |> List.map (fun x -> cs @ [x])
        )
    ) (list |> List.map (fun x -> [x]))

// 組み合わせ：順列：サンプル
combineSequence [1..4]
|> Seq.iter (printfn "%A")


// 配列の前後に最初と最後の要素を追加
let reinforce: 'a [] -> 'a [] =
  fun arr -> Array.concat [[| arr.[0] |]; arr; [| arr.[arr.Length - 1] |]]

// 連続する同値を除去
let looseDistinct: 'a [] -> 'a [] =
  fun arr ->
    Seq.foldBack (
      fun (i, x) acc ->
        if i > 0 && arr.[i - 1] = x
        then acc
        else x::acc
    ) (arr |> Array.mapi (fun i x -> i, x)) []
    |> Seq.toArray


// 配列の更新
let updateArray : int -> 'a -> 'a [] -> 'a [] =
  fun i v arr ->
    arr
    |> foldi (
      fun acc i2 _ ->
        if i = i2
        then Array.concat [ acc.[0..(i - 1)]; [| v |]; acc.[(i + 1)..] ]
        else acc
    ) arr

// ２次元配列の作成
let createArray2 : int -> int -> 'a -> 'a [] [] =
  fun iSize jSize value ->
    value
    |> Array.create jSize
    |> Array.create iSize

// ２次元配列の更新
let updateArray2 : int -> int -> 'a -> 'a [] [] -> 'a [] [] =
  fun i j v arr ->
    arr
    |> foldi (
      fun acc i2 _ ->
        if i = i2
        then Array.concat [ acc.[0..(i - 1)]; [| (acc.[i] |> updateArray j v) |]; acc.[(i + 1)..] ]
        else acc
    ) arr

// タプルのリストから２次元配列へ変換
let toArray2 : seq<int * int> -> int [] [] =
  fun  arr ->
    (
      arr |> Seq.map (fun (x, _) -> x) |> Seq.max,
      arr |> Seq.map (fun (_, y) -> y) |> Seq.max
    )
    |> fun (iSize, jSize) ->
      arr
      |> Seq.fold (
         fun acc (x, y) -> acc |> updateArray2 x y 1
      ) (createArray2 iSize jSize 0)

// セグメントごとに配列を分割
let dividArray : int -> int -> 'a [] -> 'a [] [] =
  fun length width arr ->
    arr
    |> foldi (
      fun acc i elem ->
        i / width
        |> fun i2 ->
          acc
          |> updateArray i2 (
            Array.append acc.[i2] [| elem |]
          )
    ) (Array.create length [||])


// ----- マッチング -----

let match0 =
  fun x -> 
    match x with
    | _ when x > 0 -> x
    | _ -> 0
  |> printfn "%d"

let (| EVEN | ODD |) x =
  if x % 2 = 0 then EVEN else ODD

let matchEO =
  fun x ->
    match x with
    | EVEN -> "EVEN"
    | ODD  -> "ODD"

let (| POSITIVE | NEGATIVE |) x =
  if x >= 0 then POSITIVE else NEGATIVE

let matchPN =
  fun x ->
    match x with
    | POSITIVE -> "POSITIVE"
    | NEGATIVE -> "NEGATIVE"


// ----- 関数 -----

// 2点間の距離
let distance = fun (x1, y1) (x2, y2) -> ((x2 - x1) ** 2. + (y2 - y1) ** 2.) |> sqrt

// 正方形ができるかどうか
let canFormSquare =
  fun set (x1, y1) (x2, y2) ->
    (
      (set |> Set.contains (x1 + y1 - y2, y1 + x2 - x1)) &&
      (set |> Set.contains (x2 + y1 - y2, y2 + x2 - x1))
    ) || (
      (set |> Set.contains (x1 - y1 + y2, y1 - x2 + x1)) &&
      (set |> Set.contains (x2 - y1 + y2, y2 - x2 + x1))
    )
