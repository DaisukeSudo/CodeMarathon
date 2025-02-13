// https://atcoder.jp/contests/chokudai_S001/tasks/chokudai_S001_h

let segTree<'T> (n: int) (f: 'T -> 'T -> 'T) (unity: 'T) =
  let mutable rSize = 1
  let data =
    while rSize < n do rSize <- rSize * 2
    Array.create (2 * rSize) unity

  let update (a: int) (v: 'T) =
    let mutable k = a + rSize
    data.[k] <- v
    while k > 1 do
      k <- k / 2
      data.[k] <- f data.[k * 2] data.[k * 2 + 1]

  let get (a: int) (b: int) =
    let mutable vLeft  = unity
    let mutable vRight = unity
    let mutable iLeft  = a + rSize
    let mutable iRight = b + rSize
    while iLeft < iRight do
      if iLeft % 2 = 1 then
        vLeft <- f vLeft data.[iLeft]
        iLeft <- iLeft + 1
      if iRight % 2 = 1 then
        iRight <- iRight - 1
        vRight <- f data.[iRight] vRight
      iLeft  <- iLeft / 2
      iRight <- iRight / 2
    f vLeft vRight

  (update, get)

// ----

let n = stdin.ReadLine() |> int
let a = stdin.ReadLine().Split() |> Array.map int

let sorted = Array.distinct (Array.sort a)
let index = Array.create ((sorted |> Array.last) + 1) 0 in
  sorted |> Seq.iteri (fun i x -> index.[x] <- i)

let (update, get) = segTree (n + 1) max 0

let mutable ans = 0
for x in a do
  let h = index.[x] + 1
  let v = get 0 h
  if get h (h + 1) < v + 1 then
    update h (v + 1)
    ans <- max ans (v + 1)

ans |> stdout.WriteLine

// https://atcoder.jp/contests/chokudai_S001/submissions/62695846
