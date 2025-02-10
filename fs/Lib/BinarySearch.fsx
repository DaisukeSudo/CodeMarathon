let lowerBound<'a when 'a : comparison> (v: 'a) (arr: 'a array) =
  let mutable li = 0
  let mutable ri = arr.Length - 1
  while li <= ri do
    let mi = li + (ri - li) / 2
    if arr.[mi] >= v then
        ri <- mi - 1
    else
        li <- mi + 1
  li

// [| 1; 2; 3; 3; 3; 4; 5; 5; 6 |] |> lowerBound 3 ;;
// 2

let upperBound<'a when 'a : comparison> (v: 'a) (arr: 'a array) =
  let mutable li = 0
  let mutable ri = arr.Length - 1
  while li <= ri do
    let mi = (li + ri) / 2
    if arr.[mi] > v then
      ri <- mi - 1
    else
      li <- mi + 1
  li

// [| 1; 2; 3; 3; 3; 4; 5; 5; 6 |] |> upperBound 3 ;;
// 5
