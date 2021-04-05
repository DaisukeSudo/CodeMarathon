// https://atcoder.jp/contests/abs/tasks/abc085_b

fun main(args: Array<String>) = (
  readLine()!!
    .toInt()
    .let { n -> (1..n) }
    .map { readLine()!! }
    .distinct()
    .size
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21021514
