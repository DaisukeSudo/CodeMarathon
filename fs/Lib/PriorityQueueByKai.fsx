// ============================================================
//  Priority Queue 実装
// ============================================================
 
let priorityQueueBy<'a, 'b when 'b : comparison> (pred : 'a -> 'b) =
  let heap = System.Collections.Generic.List<struct ('a * 'b)>()
 
  let inline swap i j =
    let tmp = heap.[i]
    heap.[i] <- heap.[j]
    heap.[j] <- tmp
 
  // enqueue
  let enqueue (elem: 'a) =
    heap.Add(struct (elem, pred elem))
    let mutable i = heap.Count - 1
    while i > 0 do
      let j = (i - 1) / 2
      let struct (_, ki) = heap.[i]
      let struct (_, kj) = heap.[j]
      if ki < kj then
        swap i j
        i <- j
      else
        i <- 0 // break
 
  // dequeue
  let dequeue () =
    let last = heap.Count - 1
    let struct (head, _) = heap.[0]
    heap.[0] <- heap.[last]
    heap.RemoveAt(last)
    if heap.Count > 0 then
      let mutable i = 0
      let mutable cont = true
      while cont do
        let j = i * 2 + 1
        if j >= heap.Count then
          cont <- false
        else
          let k =
            if j + 1 >= heap.Count then j
            else
              let struct (_, kj)  = heap.[j]
              let struct (_, kj1) = heap.[j + 1]
              if kj < kj1 then j else j + 1
          let struct (_, ki) = heap.[i]
          let struct (_, kk) = heap.[k]
          if ki > kk then
            swap i k
            i <- k
          else
            cont <- false
    head
 
  (enqueue, dequeue)
 
 
// ============================================================
//  テストユーティリティ
// ============================================================
 
let mutable passCount = 0
let mutable failCount = 0
 
let check label condition =
  if condition then
    printfn "  [PASS] %s" label
    passCount <- passCount + 1
  else
    printfn "  [FAIL] %s" label
    failCount <- failCount + 1
 
let section title =
  printfn ""
  printfn "=== %s ===" title
 
 
// ============================================================
//  テストケース
// ============================================================
 
// --- 1. 基本：昇順に取り出せるか ---
section "基本動作（int、昇順）"
 
let enq1, deq1 = priorityQueueBy<int, int> id
[5; 3; 8; 1; 4] |> List.iter enq1
 
check "1番目は最小値 1" (deq1() = 1)
check "2番目は 3"     (deq1() = 3)
check "3番目は 4"     (deq1() = 4)
check "4番目は 5"     (deq1() = 5)
check "5番目は 8"     (deq1() = 8)
 
 
// --- 2. 1要素のみ ---
section "1要素のみ"
 
let enq2, deq2 = priorityQueueBy<int, int> id
enq2 42
check "唯一の要素が返る" (deq2() = 42)
 
 
// --- 3. 同じ優先度の要素が複数 ---
section "同一優先度（重複値）"
 
let enq3, deq3 = priorityQueueBy<int, int> id
[2; 2; 2] |> List.iter enq3
check "1番目は 2" (deq3() = 2)
check "2番目は 2" (deq3() = 2)
check "3番目は 2" (deq3() = 2)
 
 
// --- 4. カスタム述語（文字列長で優先） ---
section "カスタム述語（文字列を長さで優先）"
 
let enq4, deq4 = priorityQueueBy<string, int> (fun s -> s.Length)
["banana"; "fig"; "apple"; "kiwi"; "date"] |> List.iter enq4
 
check "最短 \"fig\"(3)"  (deq4() = "fig")
check "次は 4文字のいずれか" (let v = deq4() in v.Length = 4)
check "次は 4文字のいずれか" (let v = deq4() in v.Length = 4)
check "次は 5文字 \"apple\"" (deq4() = "apple")
check "最後は 6文字 \"banana\"" (deq4() = "banana")
 
 
// --- 5. レコード型・述語でフィールド指定 ---
section "レコード型（.Priority フィールドで優先）"
 
type Task = { Name: string; Priority: int }
 
let enq5, deq5 = priorityQueueBy<Task, int> (fun t -> t.Priority)
[ { Name = "C"; Priority = 3 }
  { Name = "A"; Priority = 1 }
  { Name = "B"; Priority = 2 } ] |> List.iter enq5
 
check "優先度1: A" (deq5().Name = "A")
check "優先度2: B" (deq5().Name = "B")
check "優先度3: C" (deq5().Name = "C")
 
 
// --- 6. 交互に enqueue / dequeue ---
section "enqueue と dequeue の交互操作"
 
let enq6, deq6 = priorityQueueBy<int, int> id
enq6 10
enq6 5
check "初回 dequeue = 5"  (deq6() = 5)
enq6 3
enq6 8
check "次の dequeue = 3"  (deq6() = 3)
check "次の dequeue = 8"  (deq6() = 8)
check "最後の dequeue = 10" (deq6() = 10)
 
 
// --- 7. 大量データ（ソート結果の確認）---
section "大量データ（1000要素、乱数）"
 
let rng = System.Random(12345)
let data = Array.init 1000 (fun _ -> rng.Next(0, 10000))
 
let enq7, deq7 = priorityQueueBy<int, int> id
data |> Array.iter enq7
 
let result = Array.init 1000 (fun _ -> deq7())
let isSorted = result |> Array.pairwise |> Array.forall (fun (a, b) -> a <= b)
 
check "1000要素が昇順に取り出せる" isSorted
 
 
// --- 8. 負の値 ---
section "負の値を含む場合"
 
let enq8, deq8 = priorityQueueBy<int, int> id
[-3; 0; -10; 5; -1] |> List.iter enq8
 
check "最小 -10" (deq8() = -10)
check "次は -3"  (deq8() = -3)
check "次は -1"  (deq8() = -1)
check "次は 0"   (deq8() = 0)
check "最後は 5" (deq8() = 5)
 
 
// --- 9. パフォーマンス計測 ---
section "パフォーマンス計測（10万要素）"
 
let sw = System.Diagnostics.Stopwatch.StartNew()
 
let enqP, deqP = priorityQueueBy<int, int> id
let n = 100_000
let perfData = Array.init n (fun i -> n - i)  // 逆順で挿入
perfData |> Array.iter enqP
 
let perfResult = Array.init n (fun _ -> deqP())
sw.Stop()
 
let perfSorted = perfResult |> Array.pairwise |> Array.forall (fun (a, b) -> a <= b)
check "10万要素が昇順に取り出せる" perfSorted
printfn "  経過時間: %d ms" sw.ElapsedMilliseconds
 
 
// ============================================================
//  結果サマリ
// ============================================================
 
printfn ""
printfn "=============================="
printfn "  結果: %d PASS / %d FAIL" passCount failCount
printfn "=============================="
 
if failCount > 0 then
  printfn "  ⚠ 失敗したテストがあります"
  exit 1
else
  printfn "  ✓ 全テスト通過"

printfn ""
