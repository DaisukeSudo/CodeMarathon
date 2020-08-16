// https://atcoder.jp/contests/joi2007ho/tasks/joi2007ho_c

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
let toArray2 : seq<int * int> -> bool [] [] =
  fun arr ->
    (
      arr |> Seq.map (fun (x, _) -> x) |> Seq.max,
      arr |> Seq.map (fun (_, y) -> y) |> Seq.max
    )
    |> fun (iSize, jSize) ->
      arr
      |> Seq.fold (
         fun acc (x, y) -> acc |> updateArray2 x y true
      ) (createArray2 iSize jSize false)

// 値の有無：１次元配列
let hasValueArray : int ->  bool [] -> bool =
  fun i arr ->
    if i < 0 || i >= arr.Length then false else arr.[i]

// 値の有無：２次元配列
let hasValueArray2 : int -> int -> bool [] [] -> bool =
  fun i j arr ->
    if i < 0 || i >= arr.Length then false else arr.[i] |> hasValueArray j

// 正方形ができるかどうか
let canFormSquare : bool [] [] -> (int * int) * (int * int) -> bool =
  fun arr ((x1, y1), (x2, y2)) ->
    (
      (arr |> hasValueArray2 (x1 + y1 - y2) (y1 + x2 - x1)) &&
      (arr |> hasValueArray2 (x2 + y1 - y2) (y2 + x2 - x1))
    ) || (
      (arr |> hasValueArray2 (x1 - y1 + y2) (y1 - x2 + x1)) &&
      (arr |> hasValueArray2 (x2 - y1 + y2) (y2 - x2 + x1))
    )

// 正方形の面積
let squareArea : (int * int) * (int * int) -> int =
  fun ((x1, y1), (x2, y2)) ->
    (abs (x1 - x2), abs (y1 - y2))
    |> fun (a, b) -> a * a + b * b

// 間引き用の長さ
let squareLength : int -> int =
  fun area ->
    area |> float |> sqrt |> (fun num -> num / 1.4) |> ceil |> int

// 探索
let search : (int * int) list -> int =
  fun list ->
    list
    |> toArray2
    |> fun arr2 ->
      list
      |> List.fold (
        fun (area, length) (x1, y1) ->
          list
          |> List.filter (
            fun (x2, y2) ->
              x2 - x1 >= length &&
                canFormSquare arr2 ((x1, y1), (x2, y2))
          )
          |> List.map (
            fun (x2, y2) ->
              squareArea ((x1, y1), (x2, y2))
          )
          |> maxOrDefault 0
          |> fun newArea ->
            if area < newArea
            then
              (newArea, newArea |> squareLength)
            else (area, length)
      ) (0, 1)
      |> fun (area, _) -> area

// 実行
stdin.ReadLine()
|> int
|> fun x -> [ 1..x ]
|> List.map (
  fun _ ->
    stdin.ReadLine()
    |> fun x -> x.Split(' ')
    |> Array.map int
    |> fun x -> (x.[0], x.[1])
)
|> search
|> printfn "%A"

// https://atcoder.jp/contests/joi2007ho/submissions/11864779
