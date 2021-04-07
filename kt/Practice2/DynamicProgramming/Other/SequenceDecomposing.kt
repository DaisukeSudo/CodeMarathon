// https://atcoder.jp/contests/abc134/tasks/abc134_e

fun main(args: Array<String>) =
  readLine()!!
    .toInt()
    .let { n ->
      Array(n + 1) { Math.pow(10.0, 9.0).toInt() + 1 }.let { memo ->
        (1..n)
          .map { readLine()!!.toInt() }
          .fold(0) { len: Int, x: Int ->
            if (memo[len] >= x)
              Unit
                .let { memo[len + 1] = x }
                .let { len + 1 }
            else
              (1..17)
                .fold(1 to len) { acc, _ -> acc.let { (li, ri) ->
                  (li + (ri - li) / 2).let { mi ->
                    if (memo[mi] < x) (li to mi) else (mi + 1 to ri)
                  }
                }}
                .let { (i, _) -> memo[i] = x }
                .let { len }
          }
      }
    }
    .let(::println)

// https://atcoder.jp/contests/abc134/submissions/21553548
