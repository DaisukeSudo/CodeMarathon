// https://atcoder.jp/contests/abs/tasks/arc089_a

import kotlin.math.abs

fun main(args: Array<String>) = (
  readLine()!!
    .toInt()
    .let { n ->
      (1..n).map {
        readLine()!!
          .split(" ")
          .map(String::toInt)
          .let { x -> Triple(x[0], x[1], x[2])}
      }
      .fold(Triple(0, 0, 0)) { a: Triple<Int, Int, Int>?, x ->
        a?.let {
          if (((x.first and 1) == (x.second + x.third and 1))
            && ((x.first - a.first) >= (abs(x.second - a.second) + abs(x.third - a.third)))
          )
            x
          else
            null
        } 
      }
    }
    .let { it?.let { "Yes" } ?: "No" }
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21020882
