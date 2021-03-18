// https://atcoder.jp/contests/abs/tasks/abc087_b

import kotlin.math.min

fun main(args: Array<String>) = (
  Array(4) { _ -> readLine()!!.toInt() }
    .let { x ->
      (0..min(x[0], x[3] / 500))
        .map { i ->
          (0..min(x[1], (x[3] - 500 * i) / 100))
            .filter { j -> x[3] - 500 * i - 100 * j <= 50 * x[2] }
            .size
        }
        .sum()
    }
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21021840
