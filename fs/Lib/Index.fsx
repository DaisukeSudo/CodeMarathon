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
|> fun x -> x.Split()
|> Seq.map int
|> Seq.toList
|> printfn "%A"

// Ｎ件の改行区切り → 数値のリスト
stdin.ReadLine()
|> int
|> fun n -> List.init n (fun _ -> stdin.ReadLine() |> int)
|> printfn "%A"

// Ｎ件の改行区切り＋各行空白区切り → 数値のタプルのリスト
stdin.ReadLine()
|> int
|> fun n -> List.init n (fun _ -> stdin.ReadLine() |> fun x -> x.Split() |> Array.map int |> fun x -> (x.[0], x.[1]))
|> Seq.sortWith (fun (x1, _) (x2, _) -> x1 - x2)
|> printfn "%A"

// 空白区切り → 任意の個数のタプル
stdin.ReadLine()
|> fun x -> x.Split()
|> Array.map int
|> fun x -> (x.[0], x.[1], x.[2], x.[3])
|> fun (a, b, c, d) -> (a, b, c, d)
|> printfn "%A"


// ----- ファイル -----

let stdin = new System.IO.StreamReader("finepath")
stdin.ReadLine () |> printfn "%s"


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
let mean list =
  list |> Seq.averageBy float |> int

// 中央値
let median list =
  let a = list |> Seq.sort |> Seq.toArray
  let l = list |> Seq.length
  if l % 2 = 0
  then (a.[l / 2 - 1] + a.[l / 2]) / 2.
  else a.[l / 2]

// 最小値またはデフォルト
let minOrDefault : 'a -> seq<'a> -> 'a =
  fun state arr ->
    if (arr |> Seq.length > 0) then (arr |> Seq.min) else state

// 最大値またはデフォルト
let maxOrDefault : 'a -> seq<'a> -> 'a =
  fun state arr ->
    if (arr |> Seq.length > 0) then (arr |> Seq.max) else state

// index 付き fold ※ accumulator に index 入れても OK
let foldi : ('a -> int -> 'b -> 'a) -> 'a -> seq<'b> -> 'a =
  fun folder state arr ->
    arr
    |> Seq.mapi (fun i x -> i, x)
    |> Seq.fold (fun acc (i, x) -> folder acc i x) state

// 同じ値のカウント
let groupCount : (seq<'a> -> Map<'a, int>) =
  fun arr ->
    arr
    |> Seq.groupBy id
    |> Seq.map (fun (k, v) -> (k, v |> Seq.length))
    |> Map.ofSeq

// 掛け合わせ
let multiplyZip : seq<'a> -> seq<'b> -> seq<'a * 'b> =
  fun arr1 arr2 ->
    arr1
    |> Seq.collect (
      fun x1 ->
        arr2
        |> Seq.map (fun x2 -> (x1, x2))
    )

// 掛け合わせ（from < to）
let multiplyZipHalf : seq<'a> -> seq<'a> -> seq<'a * 'a> =
  fun fromArr toArr ->
    fromArr
    |> Seq.collect (
      fun x1 ->
        toArr
        |> Seq.filter (fun x2 -> x1 < x2)
        |> Seq.map (fun x2 -> (x1, x2))
    )

// 掛け合わせ：サンプル
multiplyZip [1..9] [1..9]
// |> Seq.filter (fun (a, b) -> f1 (a, b) <> f2 (a, b))
|> Seq.iter (printfn "%A")

// 順列
let permutationSequence : ('a list -> seq<'a list>) =
  let rec loop1 x = function
    | []              -> [[x]]
    | (y :: ys) as xs -> (x :: xs) :: (List.map (fun x -> y :: x) (loop1 x ys))
  let rec loop2 = function
    | []      -> seq [List.empty]
    | x :: xs -> Seq.collect (loop1 x) (loop2 xs)
  loop2

// 順列：サンプル
permutationSequence [1..4]
|> Seq.iter (printfn "%A")

// 選択パターン ※ O(2^N)
let choosePattern : seq<'a> -> seq<'a list> =
  fun arr ->
    seq {0..(1 <<< Seq.length arr) - 1} |> Seq.map (fun i ->
      arr |> Seq.fold (fun (j, acc) x ->
        if (i >>> j) &&& 1 = 1
        then (j + 1, x :: acc)
        else (j + 1, acc)
      ) (0, [])
      |> snd
      |> List.rev
    )

// 選択パターン：サンプル
choosePattern [1; 2; 3] |> Seq.toList |> printfn "%A"
// [[]; [1]; [2]; [1; 2]; [3]; [1; 3]; [2; 3]; [1; 2; 3]]

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

// 無限シーケンス
Seq.initInfinite (fun i -> (i, "ora"))
|> Seq.takeWhile (fun (i, _) -> i < 10)
|> Seq.map (fun (_, v) -> v)
|> Seq.toList
|> printfn "%A"


// ----- 配列 -----

// 番兵
let guard terminator arr =
  Array.concat [ [| terminator |]; arr; [| terminator |] ]

// 番兵（２次元配列）
let guard2 terminator count initializer =
  Array.init count (initializer >> guard terminator) |> guard (Array.create (count + 2) terminator)

// 配列の前後に最初と最後の要素を追加
let reinforce: 'a [] -> 'a [] =
  fun arr -> Array.concat [[| arr.[0] |]; arr; [| arr.[arr.Length - 1] |]]

// 配列の更新（非破壊）
let updateArray : int -> 'a -> 'a [] -> 'a [] =
  fun i v arr ->
    Array.concat [ arr.[0..(i - 1)]; [| v |]; arr.[(i + 1)..] ]

// 配列の更新（破壊）
let updateArrayB : int -> 'a -> 'a [] -> 'a [] =
  fun i v arr ->
    (arr.[i] <- v) |> fun () -> arr

// ２次元配列の作成
let createArray2 : int -> int -> 'a -> 'a [] [] =
  fun iSize jSize value ->
    Array.init iSize (fun _ -> Array.create jSize value)

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

// 配列を２倍にする
let doubleArray : 'a [] -> 'a [] =
  fun arr -> Array.concat [ arr; arr ]

// 同じ値 (int) のカウント
let groupCountToArray : (seq<int> -> int []) =
  fun arr ->
    arr
    |> Seq.groupBy id
    |> Seq.fold (
      fun acc (i, v) ->
        Array.concat [ acc.[0..(i - 1)]; [| v |> Seq.length |]; acc.[(i + 1)..] ]
      
    ) (Array.create (arr |> Seq.length) 0)


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

let matchOP =
  fun ox ->
    match ox with
    | Some x -> string x
    | None -> "None"


// ----- 再帰 -----

(
  [| fun _ _ -> 0 |]
)
|> fun (fnc: (int -> int -> int) []) ->
  (
    fun start count ->
      printfn "%d" count
      if count = 1
      then start
      else (fnc.[0] start (count - 1))
  )
  |> fun fn ->
    (fnc.[0] <- fn)
    |> fun () -> fn 123 10
|> printfn "%d"


// ----- 関数 -----

// 割って切り上げ
let roundup = fun a b -> a / b + if a % b = LanguagePrimitives.GenericZero then LanguagePrimitives.GenericZero else LanguagePrimitives.GenericOne

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

// mod p の世界
let p = 1_000_000_007L

// べき乗
let power : int64 -> int64 -> int64 =
  ([| fun _ _ -> 0L |])
  |> fun fnc ->
    (
      fun m n ->
        match n with
        | _ when n = 0L -> 1L
        | _ when n % 2L = 0L -> fnc.[0] m (n / 2L) |> (fun x -> x * x % p)
        | _ -> m * fnc.[0] m (n - 1L) % p
    )
    |> fun fn -> (fnc.[0] <- fn) |> fun () -> fn
// power 2L 10L |> printfn "%d"

// 逆元
let inverse = fun b -> power b (p - 2L)

// mod p における割り算
let modDiv = fun a b -> a * (inverse b) % p

// 順列
let permutation = fun n r -> seq {0L..(r - 1L)} |> Seq.fold (fun acc i -> acc * (n - i) % p) 1L
// permutation 6L 3L // 120L

// 組合せ
let combination = fun n r ->
  (
    permutation n r,
    permutation r r
  )
  |> fun (a, b) -> modDiv a b
// combination 6L 3L // 20L

// 階乗
// let factorial = fun n -> [2L..n] |> List.fold (fun acc i -> acc * i % p) 1L
let factorial = fun n -> permutation n (n - 1L)
// factorial 5L // 120L

// 最大公約数（再帰版）
let gcd_rec =
  [| fun _ _ -> LanguagePrimitives.GenericZero |]
  |> fun fnc ->
    (
      fun a b -> if b = LanguagePrimitives.GenericZero then a else fnc.[0] b (a % b)
    )
    |> fun fn ->
      (fnc.[0] <- fn)
      |> fun () ->
        fun a b -> fn (max a b) (min a b)

// 最大公約数（ループ版）
let gcd =
  fun a b ->
    ([| (max a b, min a b) |])
    |> fun box ->
      Seq.initInfinite (fun _ -> box.[0])
      |> Seq.takeWhile (fun (_, b) -> b > LanguagePrimitives.GenericZero)
      |> Seq.iter (
        fun (a, b) -> box.[0] <- (b, a % b)
      )
      |> fun _ -> box.[0] |> fst

// 最小公倍数
let lcm =
  fun a b ->
    a * b / gcd a b

// 何回２で割れるか
let countDivisibleBy2 =
  fun (x: int) ->
    System.Convert.ToString(x, 2)
    |> fun x -> x.Length - x.LastIndexOf('1') - 1

// 繰り返し二乗法
let binPow _a _b =
  let mutable a = _a
  let mutable b = _b
  let mutable ans = 1L
  while b <> 0L do
    if b % 2L = 1L then
      ans <- ans * a % p
    a <- a * a % p
    b <- b / 2L
  ans
