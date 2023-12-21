// https://atcoder.jp/contests/typical90/tasks/typical90_cd

//                   1 ~                  9 : n *  1  (n <=                  9 - 1) : (    1 +     9) *     9 * 1 / 2 =          45
//                  10 ~                 99 : n *  2  (n <=                 90 - 1) : (   10 +    99) *    90 * 2 / 2 =        9810
//                 100 ~                999 : n *  3  (n <=                900 - 1) : (  100 +   999) *   900 * 3 / 2 =     1483650
//                1000 ~               9999 : n *  4  (n <=               9000 - 1) : ( 1000 +  9999) *  9000 * 4 / 2 =   197982000
//               10000 ~              99999 : n *  5  (n <=              90000 - 1) : (10000 + 99999) * 90000 * 5 / 2 = 24749775000 (mod p 749774832)
//              100000 ~             999999 : n *  6  (n <=             900000 - 1)
//             1000000 ~            9999999 : n *  7  (n <=            9000000 - 1)
//            10000000 ~           99999999 : n *  8  (n <=           90000000 - 1)
//           100000000 ~          999999999 : n *  9  (n <=          900000000 - 1)
//          1000000000 ~         9999999999 : n * 10  (n <=         9000000000 - 1)
//         10000000000 ~        99999999999 : n * 11  (n <=        90000000000 - 1)
//        100000000000 ~       999999999999 : n * 12  (n <=       900000000000 - 1)
//       1000000000000 ~      9999999999999 : n * 13  (n <=      9000000000000 - 1)
//      10000000000000 ~     99999999999999 : n * 14  (n <=     90000000000000 - 1)
//     100000000000000 ~    999999999999999 : n * 15  (n <=    900000000000000 - 1)
//    1000000000000000 ~   9999999999999999 : n * 16  (n <=   9000000000000000 - 1)
//   10000000000000000 ~  99999999999999999 : n * 17  (n <=  90000000000000000 - 1)
//  100000000000000000 ~ 999999999999999999 : n * 18  (n <= 900000000000000000 - 1)
// 1000000000000000000                      : n * 19  (n <= 1)

// ----

let p = 1_000_000_007UL

let inline power m n =
  let mutable m = m
  let mutable n = n
  let mutable ret = 1UL
  while n > 0UL do
    if n &&& 1UL = 1UL then
      ret <- ret * m % p
    m <- m * m % p
    n <- n >>> 1
  ret

let inline inverse b = power b (p - 2UL)

let inline div b a = a * (inverse b) % p

// ----

let calc s e k =
  let s = s % p
  let e = e % p
  ((((s + e) % p) * (p + e - s + 1UL) % p) * k % p) |> div 2UL

let count x =
  let mutable c = 1UL
  let mutable b = 1UL
  let mutable s = 0UL
  let mutable next = true
  while next do
    next <- x >= b * 10UL
    let e = if next then (b * 10UL - 1UL) else x
    s <- (s + calc b e c) % p
    c <- c + 1UL
    b <- b * 10UL
  s

let l, r = stdin.ReadLine().Split() |> fun x -> uint64 x.[0], uint64 x.[1]

(p + (count r) - (count (l - 1UL))) % p
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/48690543
