type SegTree<'T>(n: int, f: 'T -> 'T -> 'T, unity: 'T) =
  let mutable rSize = 1
  let data =
    while rSize < n do rSize <- rSize * 2
    Array.create (2 * rSize) unity

  member this.set (a: int, v: 'T) =
    data.[a + rSize] <- v

  member this.build () =
    for k in rSize - 1 .. -1 .. 1 do
      data.[k] <- f data.[k * 2] data.[k * 2 + 1]

  member this.update (a: int, v: 'T) =
    let mutable k = a + rSize
    data.[k] <- v
    while k > 1 do
      k <- k / 2
      data.[k] <- f data.[k * 2] data.[k * 2 + 1]

  member this.get (a: int, b: int) =
    let mutable vLeft  = unity
    let mutable vRight = unity
    let mutable iLeft  = a + rSize
    let mutable iRight = b + rSize
    while iLeft < iRight do
      if iLeft % 2 = 1 then
        vLeft  <- f vLeft data.[iLeft]
        iLeft  <- iLeft + 1
      if iRight % 2 = 1 then
        iRight <- iRight - 1
        vRight <- f data.[iRight] vRight
      iLeft  <- iLeft / 2
      iRight <- iRight / 2
    f vLeft vRight

  member this.item with get(a: int) = data.[a + rSize]
