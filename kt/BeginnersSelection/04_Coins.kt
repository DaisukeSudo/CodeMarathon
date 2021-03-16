// https://atcoder.jp/contests/abs/tasks/abc087_b

import kotlin.math.min

fun main(args: Array<String>) = (
  Array(4) { _x :Int -> readLine()!!.toInt() }
  pp { arr: Array<Int>  ->
    (0..min(arr[0], arr[3] / 500))
      .map { i: Int ->
        (0..min(arr[1], (arr[3] - 500 * i) / 100))
          .filter { j: Int -> arr[3] - 500 * i - 100 * j <= 50 * arr[2] }
          .size
      }
      .sum()
  }
  pp ::println
)

infix fun <T, R> T.pp(f: (T) -> R): R = f(this)

// https://atcoder.jp/contests/abs/submissions/20985863
