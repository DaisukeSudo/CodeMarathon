// https://atcoder.jp/contests/abs/tasks/abc086_a

fun main(args: Array<String>) = (
  readLine()!!
    .split(" ")
    .map(String::toInt)
    .map { it and 1 }
    .reduce { a, x -> a and x }
    .let { if (it == 1) "Odd" else "Even" }
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21020996
